using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;


namespace AirCompany.Application.Services;
public class FlightService(
    IRepository<Flight> flightRepository,
    IRepository<AircraftModel> modelRepository,
    IMapper mapper) : IFlightService
{
    public async Task<FlightDto> Create(FlightCreateUpdateDto dto)
    {
        var entity = mapper.Map<Flight>(dto);
        entity.Id = Guid.NewGuid();
        var result = await flightRepository.Create(entity);

        return mapper.Map<FlightDto>(result);
    }

    public async Task<bool> Delete(Guid dtoId) =>
        await flightRepository.Delete(dtoId);

    public async Task<FlightDto?> Get(Guid dtoId)
    {
        var entity = await flightRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<FlightDto>(entity);
    }

    public async Task<IList<FlightDto>> GetAll() =>
        mapper.Map<IList<FlightDto>>(await flightRepository.GetAll());

    public async Task<FlightDto> Update(FlightCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await flightRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await flightRepository.Update(entity);

        return mapper.Map<FlightDto>(result);
    }

    public async Task<AircraftModelDto> GetAircraftModel(Guid flightId)
    {
        var flight = await flightRepository.Get(flightId) ?? throw new KeyNotFoundException($"Flight with Id {flightId} not found");
        var model = await modelRepository.Get(flight.AircraftModel.Id) ?? throw new KeyNotFoundException($"Model with Id {flight.AircraftModel.Id} not found");

        return mapper.Map<AircraftModelDto>(model);
    }
}
