var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo").AddDatabase("db");

var natsUserName = builder.AddParameter("NatsLogin");
var natsPassword = builder.AddParameter("NatsPassword");

var nats = builder.AddNats("nats", userName: natsUserName, password: natsPassword, port: 4222)
    .WithJetStream()
    .WithArgs("-m", "8222")
    .WithHttpEndpoint(port: 8222, targetPort: 8222);

builder.AddContainer("nats-ui", "ghcr.io/nats-nui/nui")
    .WithReference(nats)
    .WaitFor(nats)
    .WithHttpEndpoint(port: 31311, targetPort: 31311);

builder.AddProject<Projects.AirCompany_Api_Host>("aircompany-api-host")
    .WithReference(db, "aircompany")
    .WithReference(nats)
    .WaitFor(db)
    .WaitFor(nats);

builder.AddProject<Projects.AirCompany_Generator_Nats_Host>("generator")
    .WithReference(nats)
    .WaitFor(nats);

builder.Build().Run();