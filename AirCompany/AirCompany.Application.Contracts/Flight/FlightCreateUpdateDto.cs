namespace AirCompany.Application.Contracts.Flight;

/// <summary>
/// DTO for creating and updating flight information
/// </summary>
/// <param name="Code">The flight code</param>
/// <param name="DepartureAirport">The code or name of the departure airport</param>
/// <param name="ArrivalAirport">The code or name of the arrival airport</param>
/// <param name="DepartureDateTime">The scheduled departure date and time. Null indicates unknown or not scheduled</param>
/// <param name="ArrivalDateTime">The scheduled arrival date and time. Null indicates unknown or not scheduled</param>
/// <param name="Duration">The estimated flight duration. Null indicates unknown or not calculated</param>
/// <param name="AircraftModelId">The unique identifier of the aircraft model assigned to this flight</param>
public record FlightCreateUpdateDto(string Code, string DepartureAirport, string ArrivalAirport, DateTime? DepartureDateTime, DateTime? ArrivalDateTime, TimeSpan? Duration, Guid AircraftModelId);