using AirCompany.Application;
using AirCompany.Application.Services;
using AirCompany.Application.Contracts;
using AirCompany.Application.Contracts.AircraftFamily;
using AirCompany.Application.Contracts.AircraftModel;
using AirCompany.Application.Contracts.Flight;
using AirCompany.Application.Contracts.Passenger;
using AirCompany.Application.Contracts.Ticket;
using AirCompany.Domain;
using AirCompany.Domain.DataSeeder;
using AirCompany.Domain.Entities;
using AirCompany.Infrastructure.EfCore;
using AirCompany.Infrastructure.EfCore.Repository;
using AirCompany.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AirCompanyProfile());
});

builder.Services.AddSingleton<DataSeeder>();

builder.Services.AddScoped<IRepository<AircraftFamily>, AircraftFamilyEfCoreRepository>();
builder.Services.AddScoped<IRepository<AircraftModel>, AircraftModelEfCoreRepository>();
builder.Services.AddScoped<IRepository<Flight>, FlightEfCoreRepository>();
builder.Services.AddScoped<IRepository<Passenger>, PassengerEfCoreRepository>();
builder.Services.AddScoped<IRepository<Ticket>, TicketEfCoreRepository>();

builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IAircraftModelService, AircraftModelService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IApplicationService<AircraftFamilyDto, AircraftFamilyCreateUpdateDto>, AircraftFamilyService>();
builder.Services.AddScoped<IApplicationService<PassengerDto, PassengerCreateUpdateDto>, PassengerService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(a => a.GetName().Name!.StartsWith("AirCompany"))
        .Distinct();

    foreach (var assembly in assemblies)
    {
        var xmlFile = $"{assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        if (File.Exists(xmlPath))
            c.IncludeXmlComments(xmlPath);
    }

    c.UseInlineDefinitionsForEnums();
});

builder.AddMongoDBClient("aircompanyClient");

builder.Services.AddDbContext<AirCompanyDbContext>((services, o) =>
{
    var db = services.GetRequiredService<IMongoDatabase>();
    o.UseMongoDB(db.Client, db.DatabaseNamespace.DatabaseName);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AirCompanyDbContext>();
    var dataSeed = scope.ServiceProvider.GetRequiredService<DataSeeder>();


    if (!context.AircraftFamilies.Any())
    {
        foreach (var af in dataSeed.AircraftFamilies)
            await context.AircraftFamilies.AddAsync(af);

        foreach (var am in dataSeed.AircraftModels)
            await context.AircraftModels.AddAsync(am);

        foreach (var f in dataSeed.Flights)
            await context.Flights.AddAsync(f);

        foreach (var p in dataSeed.Passengers)
            await context.Passengers.AddAsync(p);

        foreach (var t in dataSeed.Tickets)
            await context.Tickets.AddAsync(t);

        await context.SaveChangesAsync();
    }
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();