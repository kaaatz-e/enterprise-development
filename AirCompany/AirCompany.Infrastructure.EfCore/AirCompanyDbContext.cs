using AirCompany.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AirCompany.Infrastructure.EfCore;

/// <summary>
/// Represents the database context for the AirCompany
/// </summary>
public class AirCompanyDbContext(DbContextOptions<AirCompanyDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Represents the collection of aircraft families in the database
    /// </summary>
    public DbSet<AircraftFamily> AircraftFamilies { get; set; }

    /// <summary>
    /// Represents the collection of aircraft models in the database
    /// </summary>
    public DbSet<AircraftModel> AircraftModels { get; set; }

    /// <summary>
    /// Represents the collection of passengers in the database
    /// </summary>
    public DbSet<Passenger> Passengers { get; set; }

    /// <summary>
    /// Represents the collection of flights in the database
    /// </summary>
    public DbSet<Flight> Flights { get; set; }

    /// <summary>
    /// Represents the collection of tickets in the database
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Configuring entities and their relationships
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;

        modelBuilder.Entity<AircraftFamily>(builder =>
        {
            builder.ToCollection("aircraft_families");

            builder.HasKey(af => af.Id);
            builder.Property(af => af.Id).HasElementName("_id");

            builder.Property(af => af.FamilyName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasElementName("family_name");

            builder.Property(af => af.Manufacturer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasElementName("manufacturer");
        });

        modelBuilder.Entity<AircraftModel>(builder =>
        {
            builder.ToCollection("aircraft_models");

            builder.HasKey(am => am.Id);
            builder.Property(am => am.Id).HasElementName("_id");

            builder.Property(am => am.ModelName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasElementName("model_name");

            builder.Property(am => am.FlightRangeKm)
                    .IsRequired()
                    .HasElementName("flight_range_km");

            builder.Property(am => am.PassengerCapacity)
                    .IsRequired()
                    .HasElementName("passenger_capacity");

            builder.Property(am => am.CargoCapacityKg)
                    .IsRequired()
                    .HasElementName("cargo_capacity_kg");

            builder.Property(m => m.AircraftFamilyId).HasElementName("family_id");
        });

        modelBuilder.Entity<Flight>(builder =>
        {
            builder.ToCollection("flights");

            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).HasElementName("_id");

            builder.Property(f => f.Code)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasElementName("code");

            builder.Property(f => f.DepartureAirport)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasElementName("departure_airport");

            builder.Property(f => f.ArrivalAirport)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasElementName("arrival_airport");

            builder.Property(f => f.DepartureDateTime)
                    .HasElementName("departure_date_time");

            builder.Property(f => f.ArrivalDateTime)
                    .HasElementName("arrival_date_time");

            builder.Property(f => f.Duration)
                    .HasElementName("duration");

            builder.Property(f => f.AircraftModelId).HasElementName("aircraft_model_id");
        });

        modelBuilder.Entity<Passenger>(builder =>
        {
            builder.ToCollection("passengers");

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasElementName("_id");

            builder.Property(p => p.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasElementName("passport_number");

            builder.Property(p => p.FullName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasElementName("full_name");

            builder.Property(p => p.BirthDate)
                    .HasElementName("birth_date");
        });

        modelBuilder.Entity<Ticket>(builder =>
        {
            builder.ToCollection("tickets");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id).HasElementName("_id");

            builder.Property(t => t.SeatNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasElementName("seat_number");

            builder.Property(t => t.HasHandLuggage)
                    .HasElementName("has_hand_luggage");

            builder.Property(t => t.TotalBaggageWeightKg)
                    .HasElementName("total_baggage_weight_kg");

            builder.Property(t => t.FlightId).HasElementName("flight_id");

            builder.Property(t => t.PassengerId).HasElementName("passenger_id");
        });
    }
}