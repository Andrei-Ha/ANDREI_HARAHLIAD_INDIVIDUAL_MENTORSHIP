using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Jobs;
using Exadel.Forecast.Api.Services;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using ModelsInterfaces = Exadel.Forecast.Models.Interfaces;
using ModelsConfig = Exadel.Forecast.Models.Configuration;
using Quartz;
using Exadel.Forecast.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey("SavingWeatherJob-Key");
    q.AddJob<SavingWeatherJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("SavingWeatherJob-Trigger")
        .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever()));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
builder.Services.Configure<CitiesSet>(builder.Configuration.GetSection("CitiesSet"));

builder.Services.AddScoped<IForecastService, ForecastService>();
builder.Services.AddScoped<ICurrentService, CurrentService>();
builder.Services.AddScoped<IValidator<string>, CityValidator>();

var weatherConfig = new ModelsConfig.Configuration();
builder.Configuration.GetSection("WeatherConfig").Bind(weatherConfig);
weatherConfig.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
weatherConfig.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
weatherConfig.WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY");

builder.Services.AddScoped<ModelsInterfaces.IConfiguration, ModelsConfig.Configuration>(p => weatherConfig);

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
