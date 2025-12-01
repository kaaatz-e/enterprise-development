using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using AirCompany.Domain.Entities;
using AutoMapper;

namespace AirCompany.Application;

/// <summary>
/// AutoMapper profile for the air company
/// </summary>
public class AirCompanyProfile : Profile
{
    /// <summary>
    /// A profile constructor that creates links between Entity and Dto classes
    /// </summary>
    public AirCompanyProfile()
    {
        CreateMap<AircraftFamily, AircraftFamilyDto>();
        CreateMap<AircraftFamilyCreateUpdateDto, AircraftFamily>();


        CreateMap<AircraftModel, AircraftModelDto>();
        CreateMap<AircraftModelCreateUpdateDto, AircraftModel>();


        CreateMap<Flight, FlightDto>();
        CreateMap<FlightCreateUpdateDto, Flight>();


        CreateMap<Passenger, PassengerDto>();
        CreateMap<PassengerCreateUpdateDto, Passenger>();


        CreateMap<Ticket, TicketDto>();
        CreateMap<TicketCreateUpdateDto, Ticket>();
    }
}
