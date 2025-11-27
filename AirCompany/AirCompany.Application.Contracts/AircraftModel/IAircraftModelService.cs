using AirCompany.Application.Contracts.AircraftFamily;

namespace AirCompany.Application.Contracts.AircraftModel;

public interface IAircraftModelService : IApplicationService<AircraftModelDto, AircraftModelCreateUpdateDto>
{

    public Task<AircraftFamilyDto> GetAircraftFamily(Guid modelId);
}