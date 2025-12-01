using AirCompany.Application.Contracts.AircraftFamily;

namespace AirCompany.Application.Contracts.AircraftModel;

/// <summary>
/// Service interface for managing aircraft model
/// </summary>
public interface IAircraftModelService : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves aircraft family information associated with a specific aircraft model
    /// </summary>
    /// <param name="modelId">The unique identifier of the aircraft model</param>
    /// <returns>Aircraft family DTO</returns>
    public Task<AircraftFamilyDto> GetAircraftFamily(Guid modelId);
}