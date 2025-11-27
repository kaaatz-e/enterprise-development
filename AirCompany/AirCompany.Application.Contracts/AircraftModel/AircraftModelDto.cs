namespace AirCompany.Application.Contracts.AircraftModel;

public record AircraftModelDto(Guid Id, string ModelName, double FlightRangeKm, int PassengerCapacity, double CargoCapacityKg);