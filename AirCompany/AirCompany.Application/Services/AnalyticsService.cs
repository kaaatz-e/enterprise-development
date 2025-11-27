using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Domain;
using AirCompany.Domain.Entities;
using AutoMapper;
using System.Linq;

namespace AirCompany.Application.Services;

public class AnalyticsService(
    IRepository<Flight> flightRepository,
    IRepository<Passenger> passengerRepository,
    IRepository<Ticket> ticketRepository,
    IMapper mapper) : IAnalyticsService
{
    public async Task<IList<FlightDto>> GetTop5FlightsByPassengerCount()
    {
        var flights = await flightRepository.GetAll();
        var tickets = await ticketRepository.GetAll();

        var topFlights = flights
            .OrderByDescending(f => tickets.Count(t => t.Flight.Id == f.Id))
            .Take(5)
            .ToList();

        return mapper.Map<IList<FlightDto>>(topFlights);
    }

    public async Task<IList<FlightDto>> GetFlightsWithMinimumDuration()
    {
        var allFlights = await flightRepository.GetAll();
        var minDuration = allFlights
            .Where(f => f.Duration.HasValue)
            .Min(f => f.Duration);

        var flights = allFlights
            .Where(f => f.Duration == minDuration)
            .OrderBy(f => f.DepartureDateTime)
            .ToList();

        return mapper.Map<IList<FlightDto>>(flights);
    }

    public async Task<IList<PassengerDto>> GetPassengersWithZeroBaggageByFlight(Guid flightId)
    {
        var tickets = await ticketRepository.GetAll();
        var passengers = await passengerRepository.GetAll();

        var passengerIds = tickets
            .Where(t => t.Flight.Id == flightId && t.TotalBaggageWeightKg == 0)
            .Select(t => t.Passenger.Id)
            .Distinct()
            .ToList();

        var result = passengers
             .Where(p => passengerIds.Contains(p.Id))
             .OrderBy(p => p.FullName)
             .ToList();

        return mapper.Map<IList<PassengerDto>>(result);
    }

    public async Task<IList<FlightDto>> GetFlightsByModelAndPeriod(Guid modelId, DateTime startPeriod, DateTime endPeriod)
    {
        var flights = await flightRepository.GetAll();

        var result = flights
            .Where(f => f.AircraftModel.Id == modelId
                        && f.DepartureDateTime >= startPeriod
                        && f.DepartureDateTime <= endPeriod)
            .ToList();

        return mapper.Map<IList<FlightDto>>(result);
    }

    public async Task<IList<FlightDto>> GetFlightsByRoute(string departureAirport, string arrivalAirport)
    {
        var flights = await flightRepository.GetAll();

        var result = flights
            .Where(f => f.DepartureAirport == departureAirport && f.ArrivalAirport == arrivalAirport)
            .ToList();

        return mapper.Map<IList<FlightDto>>(result);
    }

}