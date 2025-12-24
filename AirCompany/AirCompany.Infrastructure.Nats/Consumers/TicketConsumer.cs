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

/// <summary>
/// Background service that consumes ticket creation messages from NATS JetStream
/// </summary>
public class TicketConsumer(
    INatsConnection natsConnection,
    IServiceScopeFactory scopeFactory,
    IOptions<NatsConsumerSettings> consumerSettings,
    ILogger<TicketConsumer> logger) : BackgroundService
{
    /// <summary>
    /// Connects to JetStream and handles incoming ticket messages
    /// </summary>
    /// <param name="stoppingToken">Token triggered when the host is shutting down</param>
    /// <returns>A task representing the background operation</returns>
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
                logger.LogInformation("Consumer {ConsumerName} not found. Creating...", settings.ConsumerName);
                consumer = await js.CreateConsumerAsync(settings.StreamName, consumerConfig, ct);
                logger.LogInformation("Created consumer {ConsumerName} on stream {StreamName}", settings.ConsumerName, settings.StreamName);
            }
        }, stoppingToken);

        if (consumer == null)
        {
            logger.LogCritical("Could not initialize NATS consumer. Worker stopping");
            return;
        }

        logger.LogInformation("Ticket consumer started, listening on {Subject}", settings.SubjectName);

        await foreach (var msg in consumer.ConsumeAsync<TicketCreateUpdateDto>(cancellationToken: stoppingToken))
        {
            try
            {
                if (msg.Data == null)
                {
                    logger.LogWarning("Received an empty message, skipping");
                    await msg.AckAsync(cancellationToken: stoppingToken);
                    continue;
                }

                logger.LogInformation("Start processing Ticket for Passenger {PassengerId} (Flight {FlightId})", msg.Data.PassengerId, msg.Data.FlightId);

                await ProcessMessageAsync(msg.Data);

                await msg.AckAsync(cancellationToken: stoppingToken);

                logger.LogInformation("Successfully processed ticket");
            }
            catch (KeyNotFoundException ex)
            {
                logger.LogError("Validation failed, skipping: {Message}", ex.Message);
                await msg.AckAsync(cancellationToken: stoppingToken);
            }
            catch (OperationCanceledException)
            {
                logger.LogWarning("Processing cancelled: service is shutting down.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing ticket message, Sending NAK for redelivery");
                await msg.NakAsync(cancellationToken: stoppingToken);
            }
        }

        logger.LogInformation("Ticket consumer stopped");
    }

    /// <summary>
    /// Coordinates the business logic for creating a ticket
    /// </summary>
    /// <param name="ticket">The ticket dto received from the message queue</param>
    /// <param name="ct">Cancellation token</param>
    /// <exception cref="KeyNotFoundException">Thrown if flight or passenger records do not exist</exception>
    private async Task ProcessMessageAsync(TicketCreateUpdateDto ticket)
    {
        await using var scope = scopeFactory.CreateAsyncScope();

        var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();
        var passengerService = scope.ServiceProvider.GetRequiredService<IApplicationService<PassengerDto, PassengerCreateUpdateDto, Guid>>();
        var ticketService = scope.ServiceProvider.GetRequiredService<ITicketService>();

        await flightService.Get(ticket.FlightId);
        await passengerService.Get(ticket.PassengerId);

        var createdTicket = await ticketService.Create(ticket);

        logger.LogDebug("Database record created: TicketId {Id}", createdTicket.Id);
    }
}