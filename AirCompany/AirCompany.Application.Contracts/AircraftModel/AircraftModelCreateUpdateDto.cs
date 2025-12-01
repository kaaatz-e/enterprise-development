namespace AirCompany.Application.Contracts.AircraftModel;

/// <summary>
/// Data transfer object for creating and updating aircraft model information
/// </summary>
/// <param name="ModelName">The name of the aircraft model</param>
/// <param name="FlightRangeKm">The maximum flight range in kilometers that this aircraft model can achieve</param>
/// <param name="PassengerCapacity">The maximum number of passengers this aircraft model can accommodate</param>
/// <param name="CargoCapacityKg">The maximum cargo capacity in kilograms this aircraft model can carry</param>
/// <param name="AircraftFamily">The unique identifier of the aircraft family to which this model belongs</param>
public record AircraftModelCreateUpdateDto(string ModelName, double FlightRangeKm, int PassengerCapacity, double CargoCapacityKg, Guid AircraftFamily);
