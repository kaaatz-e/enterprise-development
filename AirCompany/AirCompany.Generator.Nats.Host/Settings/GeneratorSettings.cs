namespace AirCompany.Generator.Nats.Host.Settings;

/// <summary>
/// Configuration settings for the contract generator
/// </summary>
public class GeneratorSettings
{
    /// <summary>
    /// Interval between generations in milliseconds
    /// </summary>
    public int IntervalMs { get; set; } = 5000;

    /// <summary>
    /// Number of tickets to generate per batch
    /// </summary>
    public int TicketsPerBatch { get; set; } = 3;

    /// <summary>
    /// Flight IDs to be used for generation
    /// </summary>
    public List<Guid> FlightIds { get; set; } = [];

    /// <summary>
    /// Passenger IDs to be used for generation
    /// </summary>
    public List<Guid> PassengerIds { get; set; } = [];
}