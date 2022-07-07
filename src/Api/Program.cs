using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Services;
using ModelsInterfaces = Exadel.Forecast.Models.Interfaces;
using ModelsConfig = Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddScoped<ICurrentService, CurrentService>();
builder.Services.AddScoped<IValidator<string>, CityValidator>();

var weatherConfig = new ModelsConfig.Configuration();
builder.Configuration.GetSection("WeatherConfig").Bind(weatherConfig);
weatherConfig.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
weatherConfig.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
weatherConfig.WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY");

builder.Services.AddScoped<ModelsInterfaces.IConfiguration, ModelsConfig.Configuration>(p => weatherConfig);
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
//  builder.Services.Configure<ModelsConfig.Configuration>(builder.Configuration.GetSection("WeatherConfig"));

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
