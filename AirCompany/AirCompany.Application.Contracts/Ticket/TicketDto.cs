namespace AirCompany.Application.Contracts.Ticket;
public record TicketDto(Guid Id, string SeatNumber, bool? HasHandLuggage, double? TotalBaggageWeightKg);