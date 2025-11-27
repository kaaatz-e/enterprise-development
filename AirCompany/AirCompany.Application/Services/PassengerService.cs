using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Domain.Entities;
using AirCompany.Domain;
using AutoMapper;

namespace AirCompany.Application.Services;


public class PassengerService(
    IRepository<Passenger> passengerRepository,
    IMapper mapper): IApplicationService<PassengerDto, PassengerCreateUpdateDto>
{
    public async Task<PassengerDto> Create(PassengerCreateUpdateDto dto)
    {
        var entity = mapper.Map<Passenger>(dto);
        entity.Id = Guid.NewGuid();
        var result = await passengerRepository.Create(entity);

        return mapper.Map<PassengerDto>(result);
    }

    public async Task<bool> Delete(Guid dtoId) =>
        await passengerRepository.Delete(dtoId);

    public async Task<PassengerDto?> Get(Guid dtoId)
    {
        var entity = await passengerRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<PassengerDto>(entity);
    }

    public async Task<IList<PassengerDto>> GetAll() =>
        mapper.Map<IList<PassengerDto>>(await passengerRepository.GetAll());


    public async Task<PassengerDto> Update(PassengerCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await passengerRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await passengerRepository.Update(entity);

        return mapper.Map<PassengerDto>(result);
    }
}