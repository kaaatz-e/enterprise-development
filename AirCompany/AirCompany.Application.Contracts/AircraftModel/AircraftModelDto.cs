namespace AirCompany.Application.Contracts.AircraftModel;

/// <summary>
/// DTO for reading aircraft model information
/// </summary>
/// <param name="Id">The unique identifier of the aircraft model</param>
/// <param name="ModelName">The name of the aircraft model</param>
/// <param name="FlightRangeKm">The maximum flight range in kilometers that this aircraft model can achieve</param>
/// <param name="PassengerCapacity">The maximum number of passengers this aircraft model can accommodate</param>
/// <param name="CargoCapacityKg">The maximum cargo capacity in kilograms this aircraft model can carry</param>
public record AircraftModelDto(Guid Id, string ModelName, double FlightRangeKm, int PassengerCapacity, double CargoCapacityKg);