namespace AirCompany.Application.Contracts.AircraftFamily;


/// <summary>
/// DTO for reading aircraft family information
/// </summary>
/// <param name="Id">The unique identifier of the aircraft family</param>
/// <param name="FamilyName">The name of the aircraft family</param>
/// <param name="Manufacturer">The manufacturer of the aircraft family</param>
public record AircraftFamilyDto(Guid Id, string FamilyName, string Manufacturer);