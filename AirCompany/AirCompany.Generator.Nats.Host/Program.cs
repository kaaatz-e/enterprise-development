using AirCompany.Generator.Nats.Host.Generator;
using AirCompany.Generator.Nats.Host.Producer;
using AirCompany.Generator.Nats.Host.Settings;
using AirCompany.Generator.Nats.Host.Worker;
using AirCompany.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.Services.Configure<GeneratorSettings>(
    builder.Configuration.GetSection("Generator"));
builder.Services.Configure<NatsProducerSettings>(
    builder.Configuration.GetSection("NatsProducer"));

builder.AddNatsClient("nats");

builder.Services.AddSingleton<TicketGenerator>();
builder.Services.AddSingleton<TicketProducer>();
builder.Services.AddHostedService<TicketGeneratorWorker>();

var host = builder.Build();
host.Run();