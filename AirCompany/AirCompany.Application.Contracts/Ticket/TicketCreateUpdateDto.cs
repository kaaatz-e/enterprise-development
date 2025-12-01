namespace AirCompany.Application.Contracts.Ticket;

/// <summary>
/// DTO for creating and updating tickets
/// </summary>
/// <param name="FlightId">The unique identifier of the flight associated with the ticket</param>
/// <param name="PassengerId">The unique identifier of the passenger associated with the ticket</param>
/// <param name="SeatNumber">The seat number to assign to the ticket (e.g., "12A", "15B")</param>
/// <param name="HasHandLuggage">Indicates whether the passenger has hand luggage. Null indicates unknown status</param>
/// <param name="TotalBaggageWeightKg">The total weight of checked baggage in kilograms. Null indicates no baggage or unknown weight</param>
public record TicketCreateUpdateDto(Guid FlightId, Guid PassengerId, string SeatNumber, bool? HasHandLuggage, double? TotalBaggageWeightKg);