using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AircraftFamilyController(IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, Guid> service, ILogger<AircraftFamilyController> logger)
    : CrudControllerBase<AircraftFamilyDto, AircraftFamilyCreateUpdateDto, Guid>(service, logger);
