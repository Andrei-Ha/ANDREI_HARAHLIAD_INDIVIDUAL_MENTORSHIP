using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddScoped<ICurrentWeatherService, CurrentWeatherService>();

builder.Services.AddScoped<Exadel.Forecast.Models.Interfaces.IConfiguration, Exadel.Forecast.Models.Configuration.Configuration>(
    p => new Exadel.Forecast.Models.Configuration.Configuration()
    {
        MinAmountOfDays = builder.Configuration.GetValue<int>("Configuration:MinAmountOfDays"),
        MaxAmountOfDays = builder.Configuration.GetValue<int>("Configuration:MaxAmountOfDays"),
        DebugInfo = builder.Configuration.GetValue<bool>("Configuration:DebugInfo"),
        ExecutionTime = builder.Configuration.GetValue<int>("Configuration:ExecutionTime"),
        OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY"),
        WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY"),
        WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY")
    });

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
