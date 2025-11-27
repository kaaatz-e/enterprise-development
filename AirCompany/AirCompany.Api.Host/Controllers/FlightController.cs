using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FlightController(IFlightService service, ILogger<FlightController> logger)
    : CrudControllerBase<FlightDto, FlightCreateUpdateDto>(service, logger)
{
    [HttpGet("{id}/AircraftModel")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AircraftModelDto>> GetAircraftModel(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAircraftModel), GetType().Name, id);
        try
        {
            var res = await service.GetAircraftModel(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAircraftModel), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetAircraftModel), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAircraftModel), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
