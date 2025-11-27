namespace AirCompany.Application.Contracts.Flight;

public record FlightDto(Guid Id, string Code, string DepartureAirport, string ArrivalAirport, DateTime? DepartureDateTime, DateTime? ArrivalDateTime, TimeSpan? Duration);