using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;

namespace AirCompany.Application.Services;
public class AircraftModelService(
    IRepository<AircraftModel> modelRepository,
    IRepository<AircraftFamily> familyRepository,
    IMapper mapper) : IAircraftModelService
{
    public async Task<AircraftModelDto> Create(AircraftModelCreateUpdateDto dto)
    {
        var entity = mapper.Map<AircraftModel>(dto);
        entity.Id = Guid.NewGuid();
        var result = await modelRepository.Create(entity);

        return mapper.Map<AircraftModelDto>(result);
    }

    public async Task<bool> Delete(Guid dtoId) =>
        await modelRepository.Delete(dtoId);

    public async Task<AircraftModelDto?> Get(Guid dtoId)
    {
        var entity = await modelRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<AircraftModelDto>(entity);
    }

    public async Task<IList<AircraftModelDto>> GetAll() =>
       mapper.Map<IList<AircraftModelDto>>(await modelRepository.GetAll());

    public async Task<AircraftModelDto> Update(AircraftModelCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await modelRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await modelRepository.Update(entity);

        return mapper.Map<AircraftModelDto>(result);
    }

    public async Task<AircraftFamilyDto> GetAircraftFamily(Guid modelId)
    {
        var model = await modelRepository.Get(modelId) ?? throw new KeyNotFoundException($"Model with Id {modelId} not found");
        var family = await familyRepository.Get(model.AircraftFamily.Id) ?? throw new KeyNotFoundException($"Family with Id {model.AircraftFamily.Id} not found");

        return mapper.Map<AircraftFamilyDto>(family);
    }
}
