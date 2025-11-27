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
            AircraftFamily = families[0]
        },
        new AircraftModel 
        {
            ModelName = "A319-100", 
            PassengerCapacity = 138, 
            CargoCapacityKg = 68000, 
            FlightRangeKm = 4910, 
            AircraftFamily = families[1]
        },
        new AircraftModel 
        { 
            ModelName = "737-800", 
            PassengerCapacity = 189, 
            CargoCapacityKg = 79015, 
            FlightRangeKm = 5427, 
            AircraftFamily = families[2]
        },
        new AircraftModel 
        { 
            ModelName = "777-300ER", 
            PassengerCapacity = 408, 
            CargoCapacityKg = 317500, 
            FlightRangeKm = 11200, 
            AircraftFamily = families[3]
        },
        new AircraftModel 
        { 
            ModelName = "E170", 
            PassengerCapacity = 78, 
            CargoCapacityKg = 37200, 
            FlightRangeKm = 3800, 
            AircraftFamily = families[4]
        },
        new AircraftModel 
        { 
            ModelName = "A321NEO", 
            PassengerCapacity = 244, 
            CargoCapacityKg = 93500, 
            FlightRangeKm = 5500, 
            AircraftFamily = families[5]
        },
        new AircraftModel 
        { 
            ModelName = "A330-300", 
            PassengerCapacity = 440, 
            CargoCapacityKg = 230000, 
            FlightRangeKm = 9500, 
            AircraftFamily = families[6]
        },
        new AircraftModel 
        { 
            ModelName = "747-400", 
            PassengerCapacity = 522, 
            CargoCapacityKg = 396890, 
            FlightRangeKm = 13450, 
            AircraftFamily = families[7]
        },
        new AircraftModel 
        { 
            ModelName = "A350-900",
            PassengerCapacity = 440, 
            CargoCapacityKg = 268000, 
            FlightRangeKm = 12400, 
            AircraftFamily = families[8]
        },
        new AircraftModel 
        { 
            ModelName = "A320NEO", 
            PassengerCapacity = 149, 
            CargoCapacityKg = 75500, 
            FlightRangeKm = 4800, 
            AircraftFamily = families[9]
        }
    ];

    /// <summary>
    /// Initializes <see cref="Flight"/> with test data
    /// </summary> 
    private static List<Flight> InitFlight(List<AircraftModel> models) => [
        new Flight
        { 
            Code = "U6713",
            DepartureAirport = "SVX",
            ArrivalAirport = "DME",
            DepartureDateTime = new DateTime(2025, 10, 7, 6, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 7, 8, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModel = models[0]
        },
        new Flight
        { 
            Code = "SU2602",
            DepartureAirport = "SVO",
            ArrivalAirport = "LED",
            DepartureDateTime = new DateTime(2025, 10, 8, 7, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 8, 8, 0, 0),
            Duration = TimeSpan.FromHours(1),
            AircraftModel = models[1]
        },
        new Flight
        { 
            Code = "SU2603",
            DepartureAirport = "LED",
            ArrivalAirport = "SVO",
            DepartureDateTime = new DateTime(2025, 10, 9, 10, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 9, 11, 0, 0),
            Duration = TimeSpan.FromHours(1),
            AircraftModel = models[2]
        },
        new Flight
        { 
            Code = "S70105",
            DepartureAirport = "DME",
            ArrivalAirport = "OVB",
            DepartureDateTime = new DateTime(2025, 10, 10, 23, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 11, 3, 0, 0),
            Duration = TimeSpan.FromHours(4),
            AircraftModel = models[3]
        },
        new Flight
        {
            Code = "UT233",
            DepartureAirport = "VKO",
            ArrivalAirport = "TJM",
            DepartureDateTime = new DateTime(2025, 10, 12, 10, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 12, 13, 0, 0),
            Duration = TimeSpan.FromHours(3),
            AircraftModel = models[4]
        },
        new Flight
        {
            Code = "SU1217",
            DepartureAirport = "KUF",
            ArrivalAirport = "SVO",
            DepartureDateTime = new DateTime(2025, 10, 13, 22, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 14, 0, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModel = models[5]
        },
        new Flight
        {
            Code = "SU520",
            DepartureAirport = "SVO",
            ArrivalAirport = "DXB",
            DepartureDateTime = new DateTime(2025, 10, 15, 7, 25, 0),
            ArrivalDateTime = new DateTime(2025, 10, 15, 14, 25, 0),
            Duration = TimeSpan.FromHours(5),
            AircraftModel = models[6]
        },
        new Flight
        {
            Code = "SU2964",
            DepartureAirport = "AER",
            ArrivalAirport = "KUF",
            DepartureDateTime = new DateTime(2025, 10, 16, 12, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 16, 15, 0, 0),
            Duration = TimeSpan.FromHours(3),
            AircraftModel = models[7]
        },
        new Flight
        {
            Code = "SU214",
            DepartureAirport = "SVO",
            ArrivalAirport = "JFK",
            DepartureDateTime = new DateTime(2025, 10, 17, 1, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 17, 19, 0, 0),
            Duration = TimeSpan.FromHours(18),
            AircraftModel = models[8]
        },
        new Flight
        {
            Code = "DP751",
            DepartureAirport = "VKO",
            ArrivalAirport = "SVX",
            DepartureDateTime = new DateTime(2025, 10, 18, 16, 0, 0),
            ArrivalDateTime = new DateTime(2025, 10, 18, 18, 0, 0),
            Duration = TimeSpan.FromHours(2),
            AircraftModel = models[9]
        }
        ];

    /// <summary>
    /// Initializes <see cref="Passenger"/> with test data
    /// </summary>
    private static List<Passenger> InitPassenger() => [
        new Passenger {PassportNumber = "716546245", FullName = "Sidorova Anna Sergeevna", BirthDate = new DateOnly(2002, 5, 18)},
        new Passenger {PassportNumber = "651465468", FullName = "Alekseev V.", BirthDate = new DateOnly(1996, 7, 14)},
        new Passenger {PassportNumber = "168425152", FullName = "Kozlov Dmitry Vladimirovich", BirthDate = new DateOnly(1995, 4, 12)},
        new Passenger {PassportNumber = "425682755", FullName = "Pavlova Maria Viktorovna", BirthDate = new DateOnly(1973, 9, 18)},
        new Passenger {PassportNumber = "818928973", FullName = "Koroleva Tatiana Olegovna", BirthDate = new DateOnly(1987, 12, 8)},
        new Passenger {PassportNumber = "979812364", FullName = "Petrov P.", BirthDate = new DateOnly(1976, 3, 16)},
        new Passenger {PassportNumber = "245687261", FullName = "Nikolaev N.", BirthDate = new DateOnly(2006, 11, 30)},
        new Passenger {PassportNumber = "358143898", FullName = "Alexandrova Olga", BirthDate = new DateOnly(2004, 6, 3)},
        new Passenger {PassportNumber = "584555219", FullName = "Mikheev Vasily", BirthDate = new DateOnly(1961, 6, 25)},
        new Passenger {PassportNumber = "906442887", FullName = "Smirnov S.", BirthDate = new DateOnly(1986, 5, 19)},
        new Passenger {PassportNumber = "315764121", FullName = "Sergeev Oleg", BirthDate = new DateOnly(1993, 8, 7)},
        new Passenger {PassportNumber = "174095946", FullName = "Morozov Andrey", BirthDate = new DateOnly(2000, 10, 3)}
        ];


    /// <summary>
    /// Initializes <see cref="Ticket"/> with test data
    /// </summary>
    private static List<Ticket> InitTicket(List<Passenger> passengers, List<Flight> flights) => [
        new Ticket {Flight = flights[0], Passenger = passengers[0], SeatNumber = "12A", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[0], Passenger = passengers[1], SeatNumber = "12B", HasHandLuggage = false, TotalBaggageWeightKg = 18},
        new Ticket {Flight = flights[0], Passenger = passengers[2], SeatNumber = "12C", HasHandLuggage = true, TotalBaggageWeightKg = 0},

        new Ticket {Flight = flights[1], Passenger = passengers[3], SeatNumber = "15D", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[1], Passenger = passengers[4], SeatNumber = "15E", HasHandLuggage = false, TotalBaggageWeightKg = 7},
        new Ticket {Flight = flights[1], Passenger = passengers[5], SeatNumber = "15F", HasHandLuggage = true, TotalBaggageWeightKg = 10},

        new Ticket {Flight = flights[2], Passenger = passengers[6], SeatNumber = "7A", HasHandLuggage = false, TotalBaggageWeightKg = 13},
        new Ticket {Flight = flights[2], Passenger = passengers[7], SeatNumber = "7B", HasHandLuggage = false, TotalBaggageWeightKg = 25},

        new Ticket {Flight = flights[3], Passenger = passengers[10], SeatNumber = "21C", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[3], Passenger = passengers[11], SeatNumber = "21D", HasHandLuggage = false, TotalBaggageWeightKg = 12},

        new Ticket {Flight = flights[4], Passenger = passengers[4], SeatNumber = "5A", HasHandLuggage = false, TotalBaggageWeightKg = 17},
        new Ticket {Flight = flights[4], Passenger = passengers[8], SeatNumber = "5B", HasHandLuggage = true, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[4], Passenger = passengers[10], SeatNumber = "5C", HasHandLuggage = false, TotalBaggageWeightKg = 14},

        new Ticket {Flight = flights[5], Passenger = passengers[0], SeatNumber = "32A", HasHandLuggage = true, TotalBaggageWeightKg = 5},
        new Ticket {Flight = flights[5], Passenger = passengers[5], SeatNumber = "32B", HasHandLuggage = true, TotalBaggageWeightKg = 8},

        new Ticket {Flight = flights[6], Passenger = passengers[1], SeatNumber = "14E", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[6], Passenger = passengers[2], SeatNumber = "14F", HasHandLuggage = true, TotalBaggageWeightKg = 15},

        new Ticket {Flight = flights[7], Passenger = passengers[9], SeatNumber = "18D", HasHandLuggage = false, TotalBaggageWeightKg = 0},
        new Ticket {Flight = flights[7], Passenger = passengers[10], SeatNumber = "18C", HasHandLuggage = true, TotalBaggageWeightKg = 9},
        new Ticket {Flight = flights[7], Passenger = passengers[11], SeatNumber = "18A", HasHandLuggage = true, TotalBaggageWeightKg = 11},
        new Ticket {Flight = flights[7], Passenger = passengers[11], SeatNumber = "18E", HasHandLuggage = true, TotalBaggageWeightKg = 16}
        ];
}