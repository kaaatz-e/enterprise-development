namespace AirCompany.Application.Contracts.Flight;

public record FlightCreateUpdateDto(string Code, string DepartureAirport, string ArrivalAirport, DateTime? DepartureDateTime, DateTime? ArrivalDateTime, TimeSpan? Duration, Guid AircraftModelId);