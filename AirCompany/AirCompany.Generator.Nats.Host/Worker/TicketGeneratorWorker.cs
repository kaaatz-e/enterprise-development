using AirCompany.Generator.Nats.Host.Generator;
using AirCompany.Generator.Nats.Host.Producer;
using AirCompany.Generator.Nats.Host.Settings;
using Microsoft.Extensions.Options;

namespace AirCompany.Generator.Nats.Host.Worker;

/// <summary>
/// Background worker that orchestrates ticket generation and publishing
/// </summary>
public class TicketGeneratorWorker(
    TicketGenerator generator,
    TicketProducer producer,
    IOptions<GeneratorSettings> settings,
    ILogger<TicketGeneratorWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = settings.Value;

        logger.LogInformation("Initializing NATS JetStream...");
        await producer.InitializeStreamAsync(stoppingToken);

        logger.LogInformation("Ticket generator worker started. Interval: {Interval}ms, BatchSize: {BatchSize}", config.IntervalMs, config.TicketsPerBatch);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var tickets = generator.Generate(config.TicketsPerBatch);

                await producer.PublishBatchAsync(tickets, stoppingToken);

                await Task.Delay(config.IntervalMs, stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                logger.LogWarning("Generator worker cancelled: service is shutting down");
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in ticket batch processing: {ex}", ex.Message);
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        logger.LogInformation("Ticket generator worker stopped");
    }
}