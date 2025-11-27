var builder = DistributedApplication.CreateBuilder(args);

var db = builder.AddMongoDB("mongo").AddDatabase("db");

builder.AddProject<Projects.AirCompany_Api_Host>("aircompany-api-host")
    .WithReference(db, "aircompany")
    .WaitFor(db);

builder.Build().Run();