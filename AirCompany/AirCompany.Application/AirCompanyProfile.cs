using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using AirCompany.Domain.Entities;
using AutoMapper;

namespace AirCompany.Application;

public class AirCompanyProfile : Profile
{
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
