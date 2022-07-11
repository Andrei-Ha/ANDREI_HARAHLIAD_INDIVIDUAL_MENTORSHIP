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
        private readonly IOptionsMonitor<CitiesSet> _optionsDelegate;
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly OptionsHandler _optionsHandler;
        public SavingWeatherJob(
            ILogger<SavingWeatherJob> logger,
            IOptionsMonitor<CitiesSet> optionsDelegate,
            ISchedulerFactory schedulerFactory,
            OptionsHandler optionsHandler) 
        {
            _logger = logger;
            _optionsDelegate = optionsDelegate;
            _schedulerFactory = schedulerFactory;
            _optionsHandler = optionsHandler;
        }
        public Task Execute(IJobExecutionContext context)
        {
            //var citiesSet = _optionsDelegate.CurrentValue.Cities;
            //foreach(var c in citiesSet)
            //{
            //    Console.WriteLine($"City: {c.CityName}, Timer: {c.Timer}");
            //    var newTrigger = TriggerBuilder.Create()
            //    .WithIdentity($"SavingWeatherJob-Trigger_{c.Timer}")
            //    .ForJob(context.JobDetail)
            //    .WithSimpleSchedule(x => x.WithIntervalInSeconds(c.Timer).RepeatForever())
            //    .Build();
            //    context.Scheduler.ScheduleJob(newTrigger);
            //}

            string triggerKey = context.Trigger.Key.Name;
            _logger.LogInformation($"Hello World! Time: {DateTime.Now.ToLongTimeString()}, TriggerKey: {triggerKey}");
            return Task.CompletedTask;
        }
    }
}
