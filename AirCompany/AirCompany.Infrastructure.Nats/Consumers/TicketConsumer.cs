using System.Text.Json;
using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using AirCompany.Infrastructure.Nats.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using Polly;

namespace AirCompany.Infrastructure.Nats.Consumers;

public class TicketConsumer(
    INatsConnection natsConnection,
    IServiceScopeFactory scopeFactory,
    IOptions<NatsConsumerSettings> consumerSettings,
    ILogger<TicketConsumer> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var settings = consumerSettings.Value;

        var retryPipeline = new ResiliencePipelineBuilder()
            .AddRetry(new Polly.Retry.RetryStrategyOptions
            {
                MaxRetryAttempts = settings.MaxRetryAttempts,
                Delay = TimeSpan.FromSeconds(settings.RetryDelaySeconds),
                BackoffType = DelayBackoffType.Exponential,
                OnRetry = args =>
                {
                    logger.LogWarning("Retry {AttemptNumber} to connect to NATS JetStream consumer. Waiting {Delay}ms",
                        args.AttemptNumber, args.RetryDelay.TotalMilliseconds);
                    return ValueTask.CompletedTask;
                }
            })
            .Build();

        INatsJSConsumer? consumer = null;

        await retryPipeline.ExecuteAsync(async ct =>
        {
            var js = new NatsJSContext((NatsConnection)natsConnection);

            var consumerConfig = new ConsumerConfig(settings.ConsumerName)
            {
                DurableName = settings.ConsumerName,
                AckPolicy = ConsumerConfigAckPolicy.Explicit,
                FilterSubject = settings.SubjectName
            };

            try
            {
                consumer = await js.GetConsumerAsync(settings.StreamName, settings.ConsumerName, ct);
                logger.LogInformation("Connected to existing consumer {ConsumerName}", settings.ConsumerName);
            }
            catch (NatsJSApiException ex) when (ex.Error.Code == 404)
            {
                consumer = await js.CreateConsumerAsync(settings.StreamName, consumerConfig, ct);
                logger.LogInformation("Created consumer {ConsumerName} on stream {StreamName}", settings.ConsumerName, settings.StreamName);
            }
        }, stoppingToken);

        if (consumer == null)
        {
            logger.LogError("Failed to create or get NATS consumer");
            return;
        }

        logger.LogInformation("Ticket consumer started, listening on {Subject}", settings.SubjectName);

        await foreach (var msg in consumer.ConsumeAsync<string>(cancellationToken: stoppingToken))
        {
            try
            {
                if (msg.Data == null)
                {
                    await msg.AckAsync(cancellationToken: stoppingToken);
                    continue;
                }

                var ticket = JsonSerializer.Deserialize<TicketCreateUpdateDto>(msg.Data);
                if (ticket == null)
                {
                    logger.LogWarning("Failed to deserialize ticket message");
                    await msg.AckAsync(cancellationToken: stoppingToken);
                    continue;
                }

                await using var scope = scopeFactory.CreateAsyncScope();
                var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();
                var passengerService = scope.ServiceProvider.GetRequiredService<IApplicationService<PassengerDto, PassengerCreateUpdateDto, Guid>>();
                var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

                try
                {
                    var flight = await flightService.Get(ticket.FlightId);
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogWarning(ex, "Skipping ticket: Flight {FlightId} not found", ticket.FlightId);
                    await msg.AckAsync(cancellationToken: stoppingToken);
                    continue;
                }

                try
                {
                    var passenger = await passengerService.Get(ticket.PassengerId);
                }
                catch (KeyNotFoundException ex)
                {
                    logger.LogWarning(ex, "Skipping ticket: Passenger {PassengerId} not found", ticket.PassengerId);
                    await msg.AckAsync(cancellationToken: stoppingToken);
                    continue;
                }

                var createdTicket = await ticketService.Create(ticket);
                logger.LogInformation("Created ticket {TicketId}: FlightId={FlightId}, PassengerId={PassengerId}, Seat={Seat}", 
                    createdTicket.Id, ticket.FlightId, ticket.PassengerId, ticket.SeatNumber);

                await msg.AckAsync(cancellationToken: stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ticket message");
                await msg.AckAsync(cancellationToken: stoppingToken);
            }
        }

        logger.LogInformation("Ticket consumer stopped");
    }
}