using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController(ITicketService service, ILogger<TicketController> logger)
    : CrudControllerBase<TicketDto, TicketCreateUpdateDto, Guid>(service, logger)
{
    [HttpGet("{id}/Flight")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<FlightDto>> GetFlight(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetFlight), GetType().Name, id);
        try
        {
            var res = await service.GetFlight(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetFlight), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetFlight), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetFlight), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    [HttpGet("{id}/Passenger")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<PassengerDto>> GetPassenger(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetPassenger), GetType().Name, id);
        try
        {
            var res = await service.GetPassenger(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetPassenger), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetPassenger), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetPassenger), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
