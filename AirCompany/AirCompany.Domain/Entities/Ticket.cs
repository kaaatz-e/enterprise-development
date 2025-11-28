namespace AirCompany.Domain.Entities;

/// <summary>
/// Represents a ticket issued to a <see cref="Passenger"/> for a specific <see cref="Flight"/>
/// </summary>
public class Ticket
{
    /// <summary>
    /// The unique identifier for the ticket
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// <see cref="Flight"/> associated with this ticket
    /// </summary>
    public Flight? Flight { get; set; }

    public required Guid FlightId { get; set; }

    /// <summary>
    /// <see cref="Passenger"/> who owns this ticket
    /// </summary>
    public Passenger? Passenger { get; set; }

    public required Guid PassengerId { get; set; }

    /// <summary>
    /// The seat number assigned to this ticket
    /// </summary>
    public required string SeatNumber { get; set; }

    /// <summary>
    /// Indicates whether the <see cref="Passenger"/> has hand luggage for this ticket
    /// </summary>
    public bool? HasHandLuggage { get; set; }

    /// <summary>
    /// Total baggage weight (in kilograms) for this ticket
    /// </summary>
    public double? TotalBaggageWeightKg { get; set; }
}
