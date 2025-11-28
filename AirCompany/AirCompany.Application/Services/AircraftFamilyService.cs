using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Domain.Entities;
using AirCompany.Domain;
using AutoMapper;
using AirCompany.Application.Contracts;

namespace AirCompany.Application.Services;
public class AircraftFamilyService(
    IRepository<AircraftFamily, Guid> familyRepository,
    IMapper mapper) : IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, Guid>
{
    public async Task<AircraftFamilyDto> Create(AircraftFamilyCreateUpdateDto dto)
    {
        var entity = mapper.Map<AircraftFamily>(dto);
        var result = await familyRepository.Create(entity);

        return mapper.Map<AircraftFamilyDto>(result);
    }

    public async Task<bool> Delete(Guid dtoId) => 
        await familyRepository.Delete(dtoId);

    public async Task<AircraftFamilyDto?> Get(Guid dtoId)
    {
        var entity = await familyRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<AircraftFamilyDto>(entity);
    }

    public async Task<IList<AircraftFamilyDto>> GetAll() =>
      mapper.Map<IList<AircraftFamilyDto>>(await familyRepository.GetAll());

    public async Task<AircraftFamilyDto> Update(AircraftFamilyCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await familyRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await familyRepository.Update(entity);

        return mapper.Map<AircraftFamilyDto>(result);
    }
}
