namespace AirCompany.Application.Contracts.AircraftModel;
public record AircraftModelCreateUpdateDto(string ModelName, double FlightRangeKm, int PassengerCapacity, double CargoCapacityKg, Guid AircraftFamily);
