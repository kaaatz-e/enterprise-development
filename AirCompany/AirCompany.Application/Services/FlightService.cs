using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;


namespace AirCompany.Application.Services;

/// <summary>
/// A service for managing flights and obtaining related models
/// </summary>
/// <param name="flightRepository">Repository for operations with flight entities</param>
/// <param name="modelRepository">Repository for operations with aircraft model entities</param>
/// <param name="mapper">A mapper for converting domain models to DTO and back</param>
public class FlightService(
    IRepository<Flight, Guid> flightRepository,
    IRepository<AircraftModel, Guid> modelRepository,
    IMapper mapper) : IFlightService
{
    /// <inheritdoc/>
    public async Task<FlightDto> Create(FlightCreateUpdateDto dto)
    {
        var entity = mapper.Map<Flight>(dto);
        var result = await flightRepository.Create(entity);

        return mapper.Map<FlightDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) =>
        await flightRepository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<FlightDto?> Get(Guid dtoId)
    {
        var entity = await flightRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<FlightDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<FlightDto>> GetAll() =>
        mapper.Map<IList<FlightDto>>(await flightRepository.GetAll());

    /// <inheritdoc/>
    public async Task<FlightDto> Update(FlightCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await flightRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await flightRepository.Update(entity);

        return mapper.Map<FlightDto>(result);
    }

    /// <inheritdoc/>
    public async Task<AircraftModelDto> GetAircraftModel(Guid flightId)
    {
        var flight = await flightRepository.Get(flightId) ?? throw new KeyNotFoundException($"Flight with Id {flightId} not found");
        var model = await modelRepository.Get(flight.AircraftModelId) ?? throw new KeyNotFoundException($"Model with Id {flight.AircraftModelId} not found");

        return mapper.Map<AircraftModelDto>(model);
    }
}
