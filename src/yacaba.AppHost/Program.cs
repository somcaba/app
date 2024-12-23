var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Yacaba_Web>("yacaba-web");

builder.Build().Run();
