using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

/// <summary>
/// API Controller for managing Passenger entities
/// Inherits standard CRUD
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class PassengerController(IApplicationService<PassengerDto, PassengerCreateUpdateDto, Guid> service, ILogger<PassengerController> logger)
    : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto, Guid>(service, logger);