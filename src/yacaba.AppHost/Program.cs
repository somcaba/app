var builder = DistributedApplication.CreateBuilder(args);

#region Postgres
var postgresUsername = builder.AddParameter("postgresUsername", secret: true);
var postgresPassword = builder.AddParameter("postgresPassword", secret: true);

var postgres = builder.AddPostgres("postgres", postgresUsername, postgresPassword, port: 5432)
    .WithPgAdmin()
    .WithDataVolume(isReadOnly: false);

var postgresdb = postgres.AddDatabase("postgresdb", "yacaba");
#endregion

builder.AddProject<Projects.Yacaba_Web>("yacaba-web")
    .WithReference(postgresdb);


builder.Build().Run();
