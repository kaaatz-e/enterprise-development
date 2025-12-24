namespace AirCompany.Infrastructure.Nats.Settings;

/// <summary>
/// Configuration settings for NATS consumer
/// </summary>
public class NatsConsumerSettings
{
    /// <summary>
    /// Name of the JetStream stream
    /// </summary>
    public string StreamName { get; init; } = "TICKETS";

    /// <summary>
    /// Name of the consumer
    /// </summary>
    public string ConsumerName { get; init; } = "ticket-consumer";

    /// <summary>
    /// Subject name for consuming messages
    /// </summary>
    public string SubjectName { get; init; } = "ticket.create";

    /// <summary>
    /// Maximum retry attempts for NATS operations
    /// </summary>
    public int MaxRetryAttempts { get; init; } = 10;

    /// <summary>
    /// Delay between retries in seconds
    /// </summary>
    public int RetryDelaySeconds { get; init; } = 2;
}