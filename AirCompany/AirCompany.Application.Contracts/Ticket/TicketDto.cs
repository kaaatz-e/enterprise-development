namespace AirCompany.Application.Contracts.Ticket;

/// <summary>
/// DTO for reading ticket information
/// </summary>
/// <param name="Id">The unique identifier of the ticket</param>
/// <param name="SeatNumber">The seat number assigned to the ticket (e.g., "12A", "15B")</param>
/// <param name="HasHandLuggage">Indicates whether the passenger has hand luggage. Null indicates unknown status</param>
/// <param name="TotalBaggageWeightKg">The total weight of checked baggage in kilograms. Null indicates no baggage or unknown weight</param>
public record TicketDto(Guid Id, string SeatNumber, bool? HasHandLuggage, double? TotalBaggageWeightKg);