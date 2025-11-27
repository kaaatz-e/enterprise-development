using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AirCompany.Api.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PassengerController(IApplicationService<PassengerDto, PassengerCreateUpdateDto> service, ILogger<PassengerController> logger)
    : CrudControllerBase<PassengerDto, PassengerCreateUpdateDto>(service, logger);