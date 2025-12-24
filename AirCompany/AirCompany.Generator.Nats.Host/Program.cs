using AirCompany.Generator.Nats.Host.Generator;
using AirCompany.Generator.Nats.Host.Settings;
using AirCompany.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<GeneratorSettings>(
    builder.Configuration.GetSection("Generator"));

builder.AddNatsClient("nats");

builder.Services.AddSingleton<TicketGenerator>();

var host = builder.Build();
host.Run();
