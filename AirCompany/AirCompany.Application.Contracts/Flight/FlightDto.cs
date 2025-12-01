namespace AirCompany.Application.Contracts.Flight;

/// <summary>
/// DTO for reading flight information
/// </summary>
/// <param name="Id">The unique identifier of the flight </param>
/// <param name="Code">The flight code</param>
/// <param name="DepartureAirport">The code or name of the departure airport</param>
/// <param name="ArrivalAirport">The code or name of the arrival airport</param>
/// <param name="DepartureDateTime">The scheduled departure date and time. Null indicates unknown or not scheduled</param>
/// <param name="ArrivalDateTime">The scheduled arrival date and time. Null indicates unknown or not scheduled</param>
/// <param name="Duration">The estimated flight duration. Null indicates unknown or not calculated</param>
public record FlightDto(Guid Id, string Code, string DepartureAirport, string ArrivalAirport, DateTime? DepartureDateTime, DateTime? ArrivalDateTime, TimeSpan? Duration);