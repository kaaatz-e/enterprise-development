using AirCompany.Application.Contracts.AircraftModel;


namespace AirCompany.Application.Contracts.Flight;
public interface IFlightService: IApplicationService<FlightDto, FlightCreateUpdateDto, Guid>
{
    public Task<AircraftModelDto> GetAircraftModel(Guid flightId);
}
