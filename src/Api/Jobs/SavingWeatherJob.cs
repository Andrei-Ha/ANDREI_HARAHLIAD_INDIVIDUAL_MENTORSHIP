using Exadel.Forecast.Api.Models;
using Microsoft.Extensions.Options;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    [DisallowConcurrentExecution]
    public class SavingWeatherJob : IJob
    {
        private readonly ILogger<SavingWeatherJob> _logger;
        private readonly IOptionsMonitor<CitiesSet> _optionsDelegate;
        public SavingWeatherJob(ILogger<SavingWeatherJob> logger, IOptionsMonitor<CitiesSet> optionsDelegate) 
        {
            _logger = logger;
            _optionsDelegate = optionsDelegate;
        }
        public Task Execute(IJobExecutionContext context)
        {
            var citiesSet = _optionsDelegate.CurrentValue.Cities;
            foreach(var c in citiesSet)
            {
                Console.WriteLine($"City: {c.CityName}, Timer: {c.Timer}");
            }
            
            _logger.LogInformation($"Hello World! Time: {DateTime.Now.ToLongTimeString()}");
            return Task.CompletedTask;
        }
    }
}
