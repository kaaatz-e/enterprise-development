using AirCompany.Application.Contracts.Ticket;
using AirCompany.Generator.Nats.Host.Settings;
using Microsoft.Extensions.Options;
using NATS.Client.Core;
using NATS.Client.JetStream;
using NATS.Client.JetStream.Models;
using NATS.Client.Serializers.Json;
using NATS.Net;
using Polly;

namespace AirCompany.Generator.Nats.Host.Producer;

/// <summary>
/// Publishes ticket contracts to NATS JetStream
/// </summary>
public class TicketProducer(
    INatsConnection natsConnection,
    IOptions<NatsProducerSettings> natsSettings,
    ILogger<TicketProducer> logger)
{
    private INatsJSContext? _jsContext;
    private bool _streamInitialized;

    private readonly ResiliencePipeline _retryPipeline = new ResiliencePipelineBuilder()
        .AddRetry(new Polly.Retry.RetryStrategyOptions
        {
            MaxRetryAttempts = natsSettings.Value.MaxRetryAttempts,
            Delay = TimeSpan.FromSeconds(natsSettings.Value.RetryDelaySeconds),
            BackoffType = DelayBackoffType.Exponential,
            OnRetry = args =>
            {
                logger.LogWarning("Retry {AttemptNumber} for NATS operation. Error: {Error}",
                    args.AttemptNumber, args.Outcome.Exception?.Message);
                return ValueTask.CompletedTask;
            }
        })
        .Build();

    /// <summary>
    /// Initializes the JetStream stream if not already initialized
    /// </summary>
    public async Task InitializeStreamAsync(CancellationToken cancellationToken = default)
    {
        if (_streamInitialized)
            return;

        var settings = natsSettings.Value;

        await _retryPipeline.ExecuteAsync(async ct =>
        {
            _jsContext = natsConnection.CreateJetStreamContext();

            var streamConfig = new StreamConfig(settings.StreamName, [settings.SubjectName])
            {
                Retention = StreamConfigRetention.Limits,
                MaxAge = TimeSpan.FromHours(settings.StreamMaxAgeHours),
                Storage = StreamConfigStorage.File
            };

            try
            {
                await _jsContext.GetStreamAsync(settings.StreamName, cancellationToken: ct);
                logger.LogInformation("Stream {StreamName} already exists", settings.StreamName);
            }
            catch (NatsJSApiException ex) when (ex.Error.Code == 404)
            {
                await _jsContext.CreateStreamAsync(streamConfig, ct);
                logger.LogInformation("Created stream {StreamName} with subject {Subject}",
                    settings.StreamName, settings.SubjectName);
            }

            _streamInitialized = true;
        }, cancellationToken);
    }

    /// <summary>
    /// Publishes a batch of tickets to NATS JetStream concurrently
    /// </summary>
    /// <param name="tickets">Collection of tickets to publish</param>
    /// <param name="ct">Cancellation token</param>
    public async Task PublishBatchAsync(IEnumerable<TicketCreateUpdateDto> tickets, CancellationToken ct = default)
    {
        if (!_streamInitialized || _jsContext == null)
            throw new InvalidOperationException("Producer not initialized");

        var ticketList = tickets.ToList();
        if (ticketList.Count == 0) return;

        var tasks = ticketList.Select(ticket => PublishSingleInternalAsync(ticket, ct));

        try
        {
            await Task.WhenAll(tasks);

            logger.LogInformation("Batch published successfully. Count: {Count}", tickets.Count());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing batch to NATS");
            throw;
        }
    }

    /// <summary>
    /// Internal method to handle single message publication with retries
    /// </summary>
    private async Task PublishSingleInternalAsync(TicketCreateUpdateDto ticket, CancellationToken ct)
    {
        var subject = natsSettings.Value.SubjectName;

        await _retryPipeline.ExecuteAsync(async innerCt =>
        {
            var ack = await _jsContext!.PublishAsync(
                subject: subject,
                data: ticket,
                serializer: NatsJsonSerializer<TicketCreateUpdateDto>.Default,
                cancellationToken: innerCt);

            ack.EnsureSuccess();
        }, ct);
    }
}