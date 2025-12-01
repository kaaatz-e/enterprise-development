using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;

namespace AirCompany.Application.Services;

/// <summary>
/// A service for managing aircraft families and obtaining related models
/// </summary>
/// <param name="ticketRepository">Repository for operations with ticket entities</param>
/// <param name="flightRepository">Repository for operations with flight entities</param>
/// <param name="passengerRepository">Repository for operations with passenger entities</param>
/// <param name="mapper">A mapper for converting domain models to DTO and back</param>
public class TicketService(
    IRepository<Ticket, Guid> ticketRepository,
    IRepository<Flight, Guid> flightRepository,
    IRepository<Passenger, Guid> passengerRepository,
    IMapper mapper) : ITicketService
{
    /// <inheritdoc/>
    public async Task<TicketDto> Create(TicketCreateUpdateDto dto)
    {
        var entity = mapper.Map<Ticket>(dto);

        var last = await ticketRepository.GetAll();
        var result = await ticketRepository.Create(entity);

        return mapper.Map<TicketDto>(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(Guid dtoId) =>
        await ticketRepository.Delete(dtoId);

    /// <inheritdoc/>
    public async Task<TicketDto?> Get(Guid dtoId)
    {
        var entity = await ticketRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");

        return mapper.Map<TicketDto>(entity);
    }

    /// <inheritdoc/>
    public async Task<IList<TicketDto>> GetAll() =>
        mapper.Map<IList<TicketDto>>(await ticketRepository.GetAll());

    /// <inheritdoc/>
    public async Task<TicketDto> Update(TicketCreateUpdateDto dto, Guid dtoId)
    {
        var entity = await ticketRepository.Get(dtoId) ?? throw new KeyNotFoundException($"Entity with Id {dtoId} not found");
        mapper.Map(dto, entity);
        var result = await ticketRepository.Update(entity);

        return mapper.Map<TicketDto>(result);
    }

    /// <inheritdoc/>
    public async Task<FlightDto> GetFlight(Guid ticketId)
    {
        var ticket = await ticketRepository.Get(ticketId) ?? throw new KeyNotFoundException($"Ticket with Id {ticketId} not found");
        var flight = await flightRepository.Get(ticket.FlightId) ?? throw new KeyNotFoundException($"Flight with Id {ticket.FlightId} not found");

        return mapper.Map<FlightDto>(flight);
    }

    /// <inheritdoc/>
    public async Task<PassengerDto> GetPassenger(Guid ticketId)
    {
        var ticket = await ticketRepository.Get(ticketId) ?? throw new KeyNotFoundException($"Ticket with Id {ticketId} not found");
        var passenger = await passengerRepository.Get(ticket.PassengerId) ?? throw new KeyNotFoundException($"Passenger with Id {ticket.PassengerId} not found");

        return mapper.Map<PassengerDto>(passenger);
    }

}
