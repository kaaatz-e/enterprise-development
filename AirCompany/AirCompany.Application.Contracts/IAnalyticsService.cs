using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;

namespace AirCompany.Application.Contracts;
public interface IAnalyticsService
{
    public Task<IList<FlightDto>> GetTop5FlightsByPassengerCount();

    public Task<IList<FlightDto>> GetFlightsWithMinimumDuration();

    public Task<IList<PassengerDto>> GetPassengersWithZeroBaggageByFlight(Guid flightId);

    public Task<IList<FlightDto>> GetFlightsByModelAndPeriod(Guid modelId, DateTime startPeriod, DateTime endPeriod);
    
    public Task<IList<FlightDto>> GetFlightsByRoute(string departureAirport, string arrivalAirport);
}
