using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

/// <summary>
/// The controller for controlling Aircraft models
/// Inherits from the basic CRUD controller and adds methods for obtaining related aircraft and flight families
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AircraftModelController(IAircraftModelService service, ILogger<AircraftModelController> logger)
    : CrudControllerBase<AircraftModelDto, AircraftModelCreateUpdateDto, Guid>(service, logger)
{
    /// <summary>
    /// Getting the aircraft family associated with this model
    /// </summary>
    /// <param name="id">Model ID</param>
    /// <returns>DTO family</returns>
    [HttpGet("{id}/AircraftFamily")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<AircraftFamilyDto>> GetAircraftFamily(Guid id)
    {
        logger.LogInformation("{method} method of {controller} is called with {id} parameter", nameof(GetAircraftFamily), GetType().Name, id);
        try
        {
            var res = await service.GetAircraftFamily(id);
            logger.LogInformation("{method} method of {controller} executed successfully", nameof(GetAircraftFamily), GetType().Name);
            return Ok(res);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning("A not found exception happened during {method} method of {controller}: {@exception}", nameof(GetAircraftFamily), GetType().Name, ex);
            return NotFound($"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            logger.LogError("An exception happened during {method} method of {controller}: {@exception}", nameof(GetAircraftFamily), GetType().Name, ex);
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}
