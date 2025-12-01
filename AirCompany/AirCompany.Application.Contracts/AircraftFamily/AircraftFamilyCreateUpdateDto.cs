namespace AirCompany.Application.Contracts.AircraftFamily;

/// <summary>
/// DTO for creating and updating aircraft family information
/// </summary>
/// <param name="FamilyName">The name of the aircraft family</param>
/// <param name="Manufacturer">The manufacturer of the aircraft family</param>
public record AircraftFamilyCreateUpdateDto(string FamilyName, string Manufacturer);