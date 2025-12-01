using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;

namespace AirCompany.Application.Contracts.Ticket;

/// <summary>
/// Service for managing ticket
/// </summary>
public interface ITicketService: IApplicationService<TicketDto, TicketCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves flight information associated with a specific ticket
    /// </summary>
    /// <param name="ticketId">The unique identifier of the ticket</param>
    /// <returns>Flight DTO</returns>
    public Task<FlightDto> GetFlight(Guid ticketId);

    /// <summary>
    /// Retrieves passenger information associated with a specific ticket
    /// </summary>
    /// <param name="ticketId">The unique identifier of the ticket</param>
    /// <returns>Passenger DTO</returns>
    public Task<PassengerDto> GetPassenger(Guid ticketId);
}
