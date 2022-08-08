using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Jobs;
using Exadel.Forecast.Api.Logger;
using Exadel.Forecast.Api.Middleware;
using Exadel.Forecast.Api.Models;
using Exadel.Forecast.Api.Services;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Models;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Microsoft.EntityFrameworkCore;
using ModelsConfig = Exadel.Forecast.Models.Configuration;
using ModelsInterfaces = Exadel.Forecast.Models.Interfaces;
using Quartz;
using Microsoft.IdentityModel.Tokens;
using Exadel.Forecast.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// accepts any access token issued by identity server
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;

        options.Authority = builder.Configuration["IdentityServer:Url"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{ 
    options.AddPolicy("PostmanUser", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "postman");
        policy.RequireRole("User");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();
    var jobKey = new JobKey(nameof(CreatingTimerTriggersJob));
    q.AddJob<CreatingTimerTriggersJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity($"{nameof(CreatingTimerTriggersJob)}-Trigger").StartNow());
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-6.0
builder.Services.Configure<CitiesSet>(builder.Configuration.GetSection("CitiesSet"));

builder.Services.AddSingleton<OptionsHandler>();
builder.Services.AddScoped<SubscriptionHandler>();

builder.Services.AddScoped<IWeatherService<WeatherForecastDTO, ForecastQueryDTO>, ForecastService>();
builder.Services.AddScoped<IWeatherService<CurrentWeatherDTO, CurrentQueryDTO>, CurrentService>();
builder.Services.AddScoped<IWeatherService<WeatherHistoryDTO, HistoryQueryDTO>, HistoryService>();
builder.Services.AddScoped<IWeatherService<string, SubscriptionModel>, SubscriptionService>();
builder.Services.AddScoped<IValidator<string>, CityValidator>();
builder.Services.AddScoped<IValidator<TimeInterval>, TimeIntervalValidator>();

var weatherConfig = new ModelsConfig.Configuration();
builder.Configuration.GetSection("WeatherConfig").Bind(weatherConfig);
weatherConfig.OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY");
weatherConfig.WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY");
weatherConfig.WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY");

builder.Services.AddScoped<ModelsInterfaces.IConfiguration, ModelsConfig.Configuration>(p => weatherConfig);

builder.Services.AddAutoMapper(typeof(Program));

builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"));

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
