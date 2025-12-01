using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Domain.Entities;
using AirCompany.Domain;
using AutoMapper;

namespace AirCompany.Application.Services;

/// <summary>
/// A service for managing passengers
/// </summary>
/// <param name="passengerRepository">Repository for operations with passenger entities</param>
/// <param name="mapper">A mapper for converting domain models to DTO and back</param>
public class PassengerService(
    IRepository<Passenger, Guid> passengerRepository,
    IMapper mapper): IApplicationService<PassengerDto, PassengerCreateUpdateDto, Guid>
{
    /// <inheritdoc/>
    public async Task<PassengerDto> Create(PassengerCreateUpdateDto dto)
    {
        var entity = mapper.Map<Passenger>(dto);
        var result = await passengerRepository.Create(entity);

        return mapper.Map<PassengerDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) =>
        await passengerRepository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<PassengerDto?> Get(Guid dtoId)
    {
        var entity = await passengerRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<PassengerDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<PassengerDto>> GetAll() =>
        mapper.Map<IList<PassengerDto>>(await passengerRepository.GetAll());

    /// <inheritdoc/>
    public async Task<PassengerDto> Update(PassengerCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await passengerRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await passengerRepository.Update(entity);

        return mapper.Map<PassengerDto>(result);
    }
}