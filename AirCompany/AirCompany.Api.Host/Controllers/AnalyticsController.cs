using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

/// <summary>
/// The controller for airline analytics
/// Provides aggregated data on flights and passengers
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : Controller
{
    /// <summary>
    /// Getting the top 5 flights by number of passengers
    /// </summary>
    /// <returns>List of DTO flights</returns>
    [HttpGet("top-flights-by-passenger")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetTop5FlightsByPassengerCount()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetTop5FlightsByPassengerCount), GetType().Name);
        try
        {
            var res = await service.GetTop5FlightsByPassengerCount();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetTop5FlightsByPassengerCount), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetTop5FlightsByPassengerCount), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Getting flights with a minimum duration
    /// </summary>
    /// <returns>List of DTO flights</returns>
    [HttpGet("flights-with-minimum-duration")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<FlightDto>>> GetFlightsWithMinimumDuration()
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetFlightsWithMinimumDuration), GetType().Name);
        try
        {
            var res = await service.GetFlightsWithMinimumDuration();
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetFlightsWithMinimumDuration), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetFlightsWithMinimumDuration), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Receiving passengers on the selected flight without luggage
    /// </summary>
    /// <param name="flightId">Flight ID</param>
    /// <returns>List of DTO passengers</returns>
    [HttpGet("passengers-with-zero-baggage-by-flight")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PassengerDto>>> GetPassengersWithZeroBaggageByFlight(Guid flightId)
    {
        logger.LogInformation("{method} method of {controller} is called with {flightId} parameter", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name, flightId);
        try
        {
            var res = await service.GetPassengersWithZeroBaggageByFlight(flightId);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Getting flights of a certain aircraft model in a given period
    /// </summary>
    /// <param name="modelId">Model ID</param>
    /// <param name="startPeriod">Beginning of the period</param>
    /// <param name="endPeriod">End of the period</param>
    /// <returns>List of DTO flights</returns>
    [HttpGet("flights-by-model-and-period")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PassengerDto>>> GetFlightsByModelAndPeriod(Guid modelId, DateTime startPeriod, DateTime endPeriod)
    {
        logger.LogInformation("{method} method of {controller} is called with {modelId} parameter", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name, modelId);
        try
        {
            var res = await service.GetFlightsByModelAndPeriod(modelId, startPeriod, endPeriod);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPassengersWithZeroBaggageByFlight), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Receiving flights between two airports
    /// </summary>
    /// <param name="departureAirport">Departure airport</param>
    /// <param name="arrivalAirport">Airport of arrival</param>
    /// <returns>List of DTO flights</returns>
    [HttpGet("flights-by-route")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<List<PassengerDto>>> GetFlightsByRoute(string departureAirport, string arrivalAirport)
    {
        logger.LogInformation("{method} method of {controller} is called", nameof(GetFlightsByRoute), GetType().Name);
        try
        {
            var res = await service.GetFlightsByRoute(departureAirport, arrivalAirport);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetFlightsByRoute), GetType().Name);
            return Ok(res);
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetFlightsByRoute), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
