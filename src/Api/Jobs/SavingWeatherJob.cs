using Exadel.Forecast.Api.Models;
using Exadel.Forecast.Api.Services;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl.Matchers;

namespace Exadel.Forecast.Api.Jobs
{
    [DisallowConcurrentExecution]
    public class SavingWeatherJob : IJob
    {
        private readonly ILogger<SavingWeatherJob> _logger;
        private readonly IOptionsMonitor<CitiesSet> _optionsMonitor;
        private readonly ISchedulerFactory _schedulerFactory;
        public SavingWeatherJob(
            ILogger<SavingWeatherJob> logger,
            IOptionsMonitor<CitiesSet> optionsMonitor,
            ISchedulerFactory schedulerFactory) 
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
            _schedulerFactory = schedulerFactory;
        }
        public Task Execute(IJobExecutionContext context)
        {
            string triggerKey = context.Trigger.Key.Name;
            var arr = triggerKey.Split('_');

            if (arr.Length > 1 && int.TryParse(arr[1], out int timer))
            {
                var cityList = _optionsMonitor.CurrentValue.Cities.Where(c => c.Timer == timer).ToList();

                foreach (var city in cityList)
                {
                    _logger.LogInformation(
                        $"City: {city.Name} Time: {DateTime.Now.ToLongTimeString()}, TriggerKey: {triggerKey}, Timer: {timer}");
                }
            }

            return Task.CompletedTask;
        }
    }
}
