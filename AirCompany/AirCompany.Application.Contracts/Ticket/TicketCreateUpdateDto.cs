namespace AirCompany.Application.Contracts.Ticket;

public record TicketCreateUpdateDto(Guid FlightId, Guid PassengerId, string SeatNumber, bool? HasHandLuggage, double? TotalBaggageWeightKg);