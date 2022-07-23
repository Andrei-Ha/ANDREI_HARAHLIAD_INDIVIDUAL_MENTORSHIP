using Exadel.Forecast.IdentityServer;
using Exadel.Forecast.IdentityServer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddTestUsers(TestUsers.Users)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseIdentityServer();

app.Run();
