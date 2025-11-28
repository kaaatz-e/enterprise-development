using AirCompany.Domain.DataSeeder;
using AirCompany.Domain.Entities;
using System.Data;

namespace AirCompany.Tests;

/// <summary>
/// Contains unit tests for air company
/// Uses test data provided by <see cref="DataSeeder"/>
/// </summary>
public class AirCompanyTests(DataSeeder seed) : IClassFixture<DataSeeder>
{
    /// <summary>
    /// Test checks whether the 5 most popular flights are 
    /// displayed correctly based on the number of passengers
    /// </summary>
    [Fact]
    public void GetTop5FlightsByPassengerCount_ShouldReturnFlightsCorrectOrderByPassengerCount()
    {
        var topFlights = seed.Flights
           .Select(f => new
           {
               Flight = f.Code,
               passengerCount = seed.Tickets.Count(t => t.FlightId == f.Id)
           })
           .OrderByDescending(x => x.passengerCount)
           .Take(5)
           .ToList();

        // Assert
        Assert.NotNull(topFlights);
        Assert.Equal(5, topFlights.Count);

        var isOrdered = topFlights.SequenceEqual(topFlights.OrderByDescending(x => x.passengerCount));
        Assert.True(isOrdered, "passengerCount should be arranged in descending order");
    }

    /// <summary>
    /// Test checks whether the list of flights with the 
    /// minimum travel time is displayed correctly
    /// </summary>
    [Fact]
    public void GetFlightsWithMinimumDuration_ShouldReturnFlightsWithMinimumDuration()
    {
        var minDuration = seed.Flights.Min(f => f.Duration);
        var minDurationFlights = seed.Flights
            .Where(f => f.Duration == minDuration)
            .OrderBy(f => f.DepartureDateTime)
            .ToList();

        //Assert
        Assert.NotEmpty(minDurationFlights);
        Assert.All(minDurationFlights, flight => Assert.Equal(minDuration, flight.Duration));

        var isOrdered = minDurationFlights.SequenceEqual(minDurationFlights.OrderBy(x => x.DepartureDateTime));
        Assert.True(isOrdered, "minDurationFlights are not in chronological order by departure time");
    }

    /// <summary>
    /// Test checks whether passengers who did not 
    /// have luggage on a particular flight are returning correctly
    /// </summary>
    [Fact]
    public void GetPassengersWithZeroBaggageByFlight_ShouldReturnPassengersOrderedByName()
    {
        var flightCode = "U6713";

        var expectedIds = seed.Tickets
            .Where(t => t.Flight != null &&
                       t.Flight.Code == flightCode &&
                       t.TotalBaggageWeightKg == 0 &&
                       t.Passenger != null)
            .Select(t => t.PassengerId)
            .ToList();

        // Act
        var passengers = seed.Tickets
            .Where(t => t.Flight != null &&
                       t.Flight.Code == flightCode &&
                       t.TotalBaggageWeightKg == 0 &&
                       t.Passenger != null)
            .Select(t => t.Passenger)
            .OrderBy(p => p!.FullName)
            .ToList();

        // Assert
        Assert.Equal(expectedIds.Count, passengers.Count);
        var isOrdered = passengers.SequenceEqual(passengers.OrderBy(p => p!.FullName));
        Assert.True(isOrdered, "Passengers are not in alphabetical order by name");
    }

    /// <summary>
    /// Test checks whether information about all flights of
    /// a certain model aircraft for a specific period is returned correctly
    /// </summary>
    [Fact]
    public void GetFlightsByModelAndPeriod_ShouldReturnFlightsForSelectedModelInPeriod()
    {
        var model = seed.AircraftModels.First(m => m.ModelName.Contains("A320NEO"));
        var modelId = model.Id;
        var startPeriod = new DateTime(2025, 10, 1);
        var endPeriod = new DateTime(2025, 10, 31);

        var flight = seed.Flights
        .First(f => f.AircraftModelId == modelId && f.DepartureDateTime >= startPeriod && f.DepartureDateTime <= endPeriod);

        //Assert
        Assert.Equal(modelId, flight.AircraftModelId);
        Assert.InRange(flight.DepartureDateTime!.Value, startPeriod, endPeriod);
    }

    /// <summary>
    /// Test checks whether information about all flights 
    /// departing from the specified departure point to 
    /// the specified arrival point is returned correctly
    /// </summary>
    [Fact]
    public void GetFlightsByRoute_ShouldReturnFlightsForSelectedDepartureAndArrival()
    {
        var startAirport = "SVO";
        var endAirport = "JFK";

        var flight = seed.Flights
        .First(f => f.DepartureAirport == startAirport && f.ArrivalAirport == endAirport);

        //Assert
        Assert.Equal(startAirport, flight.DepartureAirport);
        Assert.Equal(endAirport, flight.ArrivalAirport);
    }

}