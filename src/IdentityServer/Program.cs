using Exadel.Forecast.IdentityServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseIdentityServer();

app.Run();
