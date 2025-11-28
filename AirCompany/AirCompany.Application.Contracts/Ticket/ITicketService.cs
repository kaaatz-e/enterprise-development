using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;

namespace AirCompany.Application.Contracts.Ticket;
public interface ITicketService: IApplicationService<TicketDto, TicketCreateUpdateDto, Guid>
{
    public Task<FlightDto> GetFlight(Guid ticketId);

    public Task<PassengerDto> GetPassenger(Guid ticketId);
}
