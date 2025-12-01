using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;

namespace AirCompany.Application.Contracts;

/// <summary>
/// Interface for analytics services
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Retrieves the top 5 flights with the highest number of passengers
    /// </summary>
    /// <returns>A list of flight DTOs ordered by passenger count in descending order</returns>
    public Task<IList<FlightDto>> GetTop5FlightsByPassengerCount();

    /// <summary>
    /// Retrieves flights with the minimum duration for their respective routes
    /// </summary>
    /// <returns>A list of flight DTOs representing the fastest flights for each route</returns>
    public Task<IList<FlightDto>> GetFlightsWithMinimumDuration();

    /// <summary>
    /// Retrieves passengers with zero baggage weight for a specific flight
    /// </summary>
    /// <param name="flightId">The unique identifier of the flight</param>
    /// <returns>A list of passenger DTOs who have no baggage on the specified flight</returns>
    public Task<IList<PassengerDto>> GetPassengersWithZeroBaggageByFlight(Guid flightId);

    /// <summary>
    /// Retrieves flights for a specific aircraft model within a specified time period
    /// </summary>
    /// <param name="modelId">The unique identifier of the aircraft model</param>
    /// <param name="startPeriod">The start date and time of the search period</param>
    /// <param name="endPeriod">The end date and time of the search period</param>
    /// <returns>A list of flight DTOs matching the specified criteria</returns>
    public Task<IList<FlightDto>> GetFlightsByModelAndPeriod(Guid modelId, DateTime startPeriod, DateTime endPeriod);

    /// <summary>
    /// Retrieves flights operating on a specific route between two airports
    /// </summary>
    /// <param name="departureAirport">The code or name of the departure airport</param>
    /// <param name="arrivalAirport">The code or name of the arrival airport</param>
    /// <returns>A list of flight DTOs for the specified route</returns>
    public Task<IList<FlightDto>> GetFlightsByRoute(string departureAirport, string arrivalAirport);
}