using AirCompany.Domain.Entities;

namespace AirCompany.Domain.DataSeeder;

/// <summary>
/// Provides test data for the air company. Initializes entities: <see cref=AircraftFamily"/>, 
/// <see cref="AircraftModel/">, <see cref="Flight"/>, <see cref="Passenger"/>, <see cref="Ticket"/> 
/// </summary>
public class DataSeeder
{
    public List<AircraftFamily> AircraftFamilies { get; private set; }
    public List<AircraftModel> AircraftModels { get; private set; }
    public List<Flight> Flights { get; private set; }
    public List<Passenger> Passengers { get; private set; }
    public List<Ticket> Tickets { get; private set; }

    /// <summary>
    /// Initializes DataSeeder and loads all test data
    /// </summary>
    public DataSeeder()
    {
        AircraftFamilies = InitAircraftFamily();
        AircraftModels = InitAircraftModel(AircraftFamilies);
        Flights = InitFlight(AircraftModels);
        Passengers = InitPassenger();
        Tickets = InitTicket(Passengers, Flights);
    }

    /// <summary>
    /// Initializes a <see cref="AircraftFamily"/> with test data
    /// </summary>
    private static List<AircraftFamily> InitAircraftFamily() => [
        new AircraftFamily {FamilyName = "SSJ100", Manufacturer = "Sukhoi"},
        new AircraftFamily {FamilyName = "A319", Manufacturer = "Airbus"},
        new AircraftFamily {FamilyName = "737", Manufacturer = "Boeing"},
        new AircraftFamily {FamilyName = "777", Manufacturer = "Boeing"},
        new AircraftFamily {FamilyName = "E-Jets", Manufacturer = "Embraer"},
        new AircraftFamily {FamilyName = "A321", Manufacturer = "Airbus"},
        new AircraftFamily {FamilyName = "A330", Manufacturer = "Airbus"},
        new AircraftFamily {FamilyName = "747", Manufacturer = "Boeing"},
        new AircraftFamily {FamilyName = "A350", Manufacturer = "Airbus"},
        new AircraftFamily {FamilyName = "A320", Manufacturer = "Airbus"}
        ];

    /// <summary>
    /// Initializes <see cref="AircraftModel"/> with test data
    /// </summary>
    private static List<AircraftModel> InitAircraftModel(List<AircraftFamily> families) => [
        new AircraftModel 
        {
            ModelName = "SSJ100-95LR", 
            PassengerCapacity = 103, 
            CargoCapacityKg = 49450, 
            FlightRangeKm = 4578, 
            AircraftFamilyId = families[0].Id
        },
        new AircraftModel 
        {
            ModelName = "A319-100", 
            PassengerCapacity = 138, 
            CargoCapacityKg = 68000, 
            FlightRangeKm = 4910, 
            AircraftFamilyId = families[1].Id
        },
        new AircraftModel 
        { 
            ModelName = "737-800", 
            PassengerCapacity = 189, 
            CargoCapacityKg = 79015, 
            FlightRangeKm = 5427, 
            AircraftFamilyId = families[2].Id
        },
        new AircraftModel 
        { 
            ModelName = "777-300ER", 
            PassengerCapacity = 408, 
            CargoCapacityKg = 317500, 
            FlightRangeKm = 11200, 
            AircraftFamilyId = families[3].Id
        },
        new AircraftModel 
        { 
            ModelName = "E170", 
            PassengerCapacity = 78, 
            CargoCapacityKg = 37200, 
            FlightRangeKm = 3800, 
            AircraftFamilyId = families[4].Id
        },
        new AircraftModel 
        { 
            ModelName = "A321NEO", 
            PassengerCapacity = 244, 
            CargoCapacityKg = 93500, 
            FlightRangeKm = 5500, 
            AircraftFamilyId = families[5].Id
        },
        new AircraftModel 
        { 
            ModelName = "A330-300", 
            PassengerCapacity = 440, 
            CargoCapacityKg = 230000, 
            FlightRangeKm = 9500, 
            AircraftFamilyId = families[6].Id
        },
        new AircraftModel 
        { 
            ModelName = "747-400", 
            PassengerCapacity = 522, 
            CargoCapacityKg = 396890, 
            FlightRangeKm = 13450, 
            AircraftFamilyId = families[7].Id
        },
        new AircraftModel 
        { 
            ModelName = "A350-900",
            PassengerCapacity = 440, 
            CargoCapacityKg = 268000, 
            FlightRangeKm = 12400, 
            AircraftFamilyId = families[8].Id
        },
        new AircraftModel 
        { 
            ModelName = "A320NEO", 
            PassengerCapacity = 149, 
            CargoCapacityKg = 75500, 
            FlightRangeKm = 4800, 
            AircraftFamilyId = families[9].Id
        }
    ];

    /// <summary>
    /// Initializes <see cref="Flight"/> with test data
    /// </summary> 
    private static List<Flight> InitFlight(List<AircraftModel> models) => [
        new Flight
        { 
            Id = new Guid("f1111111-1111-1111-1111-111111111111"),
            Code = "U6713",
            DepartureAirport = "SVX",
            ArrivalAirport = "DME",
            DepartureDateTime = new DateTime(2025, 10, 7, 6, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 7, 8, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModelId = models[0].Id
        },
        new Flight
        { 
            Id = new Guid("f2222222-2222-2222-2222-222222222222"),
            Code = "SU2602",
            DepartureAirport = "SVO",
            ArrivalAirport = "LED",
            DepartureDateTime = new DateTime(2025, 10, 8, 7, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 8, 8, 0, 0),
            Duration = TimeSpan.FromHours(1),
            AircraftModelId = models[1].Id
        },
        new Flight
        { 
            Id = new Guid("f3333333-3333-3333-3333-333333333333"),
            Code = "SU2603",
            DepartureAirport = "LED",
            ArrivalAirport = "SVO",
            DepartureDateTime = new DateTime(2025, 10, 9, 10, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 9, 11, 0, 0),
            Duration = TimeSpan.FromHours(1),
            AircraftModelId = models[2].Id
        },
        new Flight
        { 
            Id = new Guid("f4444444-4444-4444-4444-444444444444"),
            Code = "S70105",
            DepartureAirport = "DME",
            ArrivalAirport = "OVB",
            DepartureDateTime = new DateTime(2025, 10, 10, 23, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 11, 3, 0, 0),
            Duration = TimeSpan.FromHours(4),
            AircraftModelId = models[3].Id
        },
        new Flight
        {
            Id = new Guid("f5555555-5555-5555-5555-555555555555"),
            Code = "UT233",
            DepartureAirport = "VKO",
            ArrivalAirport = "TJM",
            DepartureDateTime = new DateTime(2025, 10, 12, 10, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 12, 13, 0, 0),
            Duration = TimeSpan.FromHours(3),
            AircraftModelId = models[4].Id
        },
        new Flight
        {
            Id = new Guid("f6666666-6666-6666-6666-666666666666"),
            Code = "SU1217",
            DepartureAirport = "KUF",
            ArrivalAirport = "SVO",
            DepartureDateTime = new DateTime(2025, 10, 13, 22, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 14, 0, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModelId = models[5].Id
        },
        new Flight
        {
            Id = new Guid("f7777777-7777-7777-7777-777777777777"),
            Code = "SU520",
            DepartureAirport = "SVO",
            ArrivalAirport = "DXB",
            DepartureDateTime = new DateTime(2025, 10, 15, 7, 25, 0),
            ArrivalDateTime = new DateTime(2025, 10, 15, 14, 25, 0),
            Duration = TimeSpan.FromHours(5),
            AircraftModelId = models[6].Id 
        },
        new Flight
        {
            Id = new Guid("f8888888-8888-8888-8888-888888888888"),
            Code = "SU2964",
            DepartureAirport = "AER",
            ArrivalAirport = "KUF",
            DepartureDateTime = new DateTime(2025, 10, 16, 12, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 16, 15, 0, 0),
            Duration = TimeSpan.FromHours(3),
            AircraftModelId = models[7].Id  
        },
        new Flight
        {
            Id = new Guid("f9999999-9999-9999-9999-999999999999"),
            Code = "SU214",
            DepartureAirport = "SVO",
            ArrivalAirport = "JFK",
            DepartureDateTime = new DateTime(2025, 10, 17, 1, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 17, 19, 0, 0),
            Duration = TimeSpan.FromHours(18),
            AircraftModelId = models[8].Id
        },
        new Flight
        {
            Id = new Guid("faaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Code = "DP751",
            DepartureAirport = "VKO",
            ArrivalAirport = "SVX",
            DepartureDateTime = new DateTime(2025, 10, 18, 16, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 18, 18, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModelId = models[9].Id
        }
        ];

    /// <summary>
    /// Initializes <see cref="Passenger"/> with test data
    /// </summary>
    private static List<Passenger> InitPassenger() => [
        new Passenger {Id = new Guid("e1111111-1111-1111-1111-111111111111"), PassportNumber = "716546245", FullName = "Sidorova Anna Sergeevna", BirthDate = new DateOnly(2002, 5, 18)},
        new Passenger {Id = new Guid("e2222222-2222-2222-2222-222222222222"), PassportNumber = "651465468", FullName = "Alekseev V.", BirthDate = new DateOnly(1996, 7, 14)},
        new Passenger {Id = new Guid("e3333333-3333-3333-3333-333333333333"), PassportNumber = "168425152", FullName = "Kozlov Dmitry Vladimirovich", BirthDate = new DateOnly(1995, 4, 12)},
        new Passenger {Id = new Guid("e4444444-4444-4444-4444-444444444444"), PassportNumber = "425682755", FullName = "Pavlova Maria Viktorovna", BirthDate = new DateOnly(1973, 9, 18)},
        new Passenger {Id = new Guid("e5555555-5555-5555-5555-555555555555"), PassportNumber = "818928973", FullName = "Koroleva Tatiana Olegovna", BirthDate = new DateOnly(1987, 12, 8)},
        new Passenger {Id = new Guid("e6666666-6666-6666-6666-666666666666"), PassportNumber = "979812364", FullName = "Petrov P.", BirthDate = new DateOnly(1976, 3, 16)},
        new Passenger {Id = new Guid("e7777777-7777-7777-7777-777777777777"), PassportNumber = "245687261", FullName = "Nikolaev N.", BirthDate = new DateOnly(2006, 11, 30)},
        new Passenger {Id = new Guid("e8888888-8888-8888-8888-888888888888"), PassportNumber = "358143898", FullName = "Alexandrova Olga", BirthDate = new DateOnly(2004, 6, 3)},
        new Passenger {Id = new Guid("e9999999-9999-9999-9999-999999999999"), PassportNumber = "584555219", FullName = "Mikheev Vasily", BirthDate = new DateOnly(1961, 6, 25)},
        new Passenger {Id = new Guid("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), PassportNumber = "906442887", FullName = "Smirnov S.", BirthDate = new DateOnly(1986, 5, 19)},
        new Passenger {Id = new Guid("ebbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), PassportNumber = "315764121", FullName = "Sergeev Oleg", BirthDate = new DateOnly(1993, 8, 7)},
        new Passenger {Id = new Guid("eccccccc-cccc-cccc-cccc-cccccccccccc"), PassportNumber = "174095946", FullName = "Morozov Andrey", BirthDate = new DateOnly(2000, 10, 3)}
        ];


    /// <summary>
    /// Initializes <see cref="Ticket"/> with test data
    /// </summary>
    private static List<Ticket> InitTicket(List<Passenger> passengers, List<Flight> flights) => [
        new Ticket {FlightId = flights[0].Id, PassengerId = passengers[0].Id, SeatNumber = "12A", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[0].Id, PassengerId = passengers[1].Id, SeatNumber = "12B", HasHandLuggage = false, TotalBaggageWeightKg = 18},
        new Ticket {FlightId = flights[0].Id, PassengerId = passengers[2].Id, SeatNumber = "12C", HasHandLuggage = true, TotalBaggageWeightKg = 0},

        new Ticket {FlightId = flights[1].Id, PassengerId = passengers[3].Id, SeatNumber = "15D", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[1].Id, PassengerId = passengers[4].Id, SeatNumber = "15E", HasHandLuggage = false, TotalBaggageWeightKg = 7},
        new Ticket {FlightId = flights[1].Id, PassengerId = passengers[5].Id, SeatNumber = "15F", HasHandLuggage = true, TotalBaggageWeightKg = 10},

        new Ticket {FlightId = flights[2].Id, PassengerId = passengers[6].Id, SeatNumber = "7A", HasHandLuggage = false, TotalBaggageWeightKg = 13},
        new Ticket {FlightId = flights[2].Id, PassengerId = passengers[7].Id, SeatNumber = "7B", HasHandLuggage = false, TotalBaggageWeightKg = 25},

        new Ticket {FlightId = flights[3].Id, PassengerId = passengers[10].Id, SeatNumber = "21C", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[3].Id, PassengerId = passengers[11].Id, SeatNumber = "21D", HasHandLuggage = false, TotalBaggageWeightKg = 12},

        new Ticket {FlightId = flights[4].Id, PassengerId = passengers[4].Id, SeatNumber = "5A", HasHandLuggage = false, TotalBaggageWeightKg = 17},
        new Ticket {FlightId = flights[4].Id, PassengerId = passengers[8].Id, SeatNumber = "5B", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[4].Id, PassengerId = passengers[10].Id, SeatNumber = "5C", HasHandLuggage = false, TotalBaggageWeightKg = 14},

        new Ticket {FlightId = flights[5].Id, PassengerId = passengers[0].Id, SeatNumber = "32A", HasHandLuggage = true, TotalBaggageWeightKg = 5},
        new Ticket {FlightId = flights[5].Id, PassengerId = passengers[5].Id, SeatNumber = "32B", HasHandLuggage = true, TotalBaggageWeightKg = 8},

        new Ticket {FlightId = flights[6].Id, PassengerId = passengers[1].Id, SeatNumber = "14E", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[6].Id, PassengerId = passengers[2].Id, SeatNumber = "14F", HasHandLuggage = true, TotalBaggageWeightKg = 15},

        new Ticket {FlightId = flights[7].Id, PassengerId = passengers[9].Id, SeatNumber = "18D", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {FlightId = flights[7].Id, PassengerId = passengers[10].Id, SeatNumber = "18C", HasHandLuggage = true, TotalBaggageWeightKg = 9},
        new Ticket {FlightId = flights[7].Id, PassengerId = passengers[11].Id, SeatNumber = "18A", HasHandLuggage = true, TotalBaggageWeightKg = 11},
        new Ticket {FlightId = flights[7].Id, PassengerId = passengers[11].Id, SeatNumber = "18E", HasHandLuggage = true, TotalBaggageWeightKg = 16}
        ];
}