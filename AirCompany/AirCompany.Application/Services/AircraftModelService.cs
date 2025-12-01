using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;

namespace AirCompany.Application.Services;

/// <summary>
///  Aircraft model management service and getting related flights
/// </summary>
/// <param name="modelRepository">Repository for operations with aircraft model entities</param>
/// <param name="familyRepository">Repository for operations with aircraft family entities</param>
/// <param name="mapper">A mapper for converting domain models to DTO and back</param>
public class AircraftModelService(
    IRepository<AircraftModel, Guid> modelRepository,
    IRepository<AircraftFamily, Guid> familyRepository,
    IMapper mapper) : IAircraftModelService
{
    /// <inheritdoc/>
    public async Task<AircraftModelDto> Create(AircraftModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<AircraftModel>(dto);
        var result = await modelRepository.Create(entity);

        return mapper.Map<AircraftModelDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) =>
        await modelRepository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<AircraftModelDto?> Get(Guid dtoId)
    {
        var entity = await modelRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<AircraftModelDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<AircraftModelDto>> GetAll() =>
       mapper.Map<IList<AircraftModelDto>>(await modelRepository.GetAll());

    /// <inheritdoc/>
    public async Task<AircraftModelDto> Update(AircraftModelCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await modelRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await modelRepository.Update(entity);

        return mapper.Map<AircraftModelDto>(result);
    }

    /// <inheritdoc/>
    public async Task<AircraftFamilyDto> GetAircraftFamily(Guid modelId)
    {
        var model = await modelRepository.Get(modelId) ?? throw new KeyNotFoundException($"Model with Id {modelId} not found");
        var family = await familyRepository.Get(model.AircraftFamilyId) ?? throw new KeyNotFoundException($"Family with Id {model.AircraftFamilyId} not found");

        return mapper.Map<AircraftFamilyDto>(family);
    }
}
