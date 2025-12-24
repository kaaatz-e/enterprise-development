namespace AirCompany.Generator.Nats.Host.Settings;

/// <summary>
/// Configuration settings for NATS JetStream Producer
/// </summary>
public class NatsProducerSettings
{
    /// <summary>
    /// Name of the JetStream stream
    /// </summary>
    public string StreamName { get; set; } = "TICKETS";

    /// <summary>
    /// Subject name for publishing messages
    /// </summary>
    public string SubjectName { get; set; } = "ticket.create";

    /// <summary>
    /// Maximum retry attempts for NATS operations
    /// </summary>
    public int MaxRetryAttempts { get; set; } = 10;

    /// <summary>
    /// Delay between retries in seconds
    /// </summary>
    public int RetryDelaySeconds { get; set; } = 2;

    /// <summary>
    /// Maximum age of messages in stream (hours)
    /// </summary>
    public int StreamMaxAgeHours { get; set; } = 24;
}
