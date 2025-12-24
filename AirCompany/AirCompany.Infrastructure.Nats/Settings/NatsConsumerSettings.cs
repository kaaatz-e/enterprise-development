namespace AirCompany.Infrastructure.Nats.Settings;

public class NatsConsumerSettings
{
    public string StreamName { get; set; } = "TICKETS";

    public string ConsumerName { get; set; } = "ticket-consumer";

    public string SubjectName { get; set; } = "ticket.create";

    public int MaxRetryAttempts { get; set; } = 10;

    public int RetryDelaySeconds { get; set; } = 2;
}