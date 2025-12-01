using AirCompany.Application.Contracts.AircraftModel;


namespace AirCompany.Application.Contracts.Flight;

/// <summary>
/// Service interface for managing flight
/// </summary>
public interface IFlightService: IApplicationService<FlightDto, FlightCreateUpdateDto, Guid>
{
    /// <summary>
    /// Retrieves aircraft model information associated with a specific flight
    /// </summary>
    /// <param name="flightId">The unique identifier of the flight</param>
    /// <returns>Aircraft model DTO</returns>
    public Task<AircraftModelDto> GetAircraftModel(Guid flightId);
}
