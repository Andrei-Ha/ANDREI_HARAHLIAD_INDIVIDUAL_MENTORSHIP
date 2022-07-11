using Exadel.Forecast.Api.Jobs;
using Exadel.Forecast.Api.Models;
using Microsoft.Extensions.Options;
using Quartz;
using Quartz.Impl.Matchers;

namespace Exadel.Forecast.Api.Services
{
    public class OptionsHandler
    {
        private readonly IOptionsMonitor<CitiesSet> _optionsMonitor;
        private readonly ISchedulerFactory _schedulerFactory;
        private CitiesSet _citiesSet = new();

        public OptionsHandler (IOptionsMonitor<CitiesSet> optionsMonitor, ISchedulerFactory schedulerFactory)
        {
            _optionsMonitor = optionsMonitor;
            _optionsMonitor.OnChange(UpdateTriggers);
            _schedulerFactory = schedulerFactory;
        }

        private void UpdateTriggers(CitiesSet citiesSet, string str)
        {
            Console.WriteLine("UpdateTrigger");
            var scheduler = _schedulerFactory.GetScheduler().Result;
            var jobDetail = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup()).Result.FirstOrDefault();
            Console.WriteLine($"JobName:{jobDetail.Name}");
            var allTriggerKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

            if (_citiesSet.GetAllAsString() != citiesSet.GetAllAsString())
            {
                Console.WriteLine("ConfigChanged");
                _citiesSet = citiesSet;
                foreach (var triggerKey in allTriggerKeys.Result)
                {
                    Console.WriteLine($"TriggerKey: {triggerKey}");
                    //scheduler.UnscheduleJob(triggerKey);
                }
                foreach (var city in _citiesSet.Cities)
                {
                    var newTrigger = TriggerBuilder.Create()
                        .WithIdentity($"SavingWeatherJob-Trigger_{city.Timer}")
                        .ForJob(jobKey: jobDetail)
                        .WithSimpleSchedule(x => x.WithIntervalInSeconds(city.Timer).RepeatForever())
                        .Build();
                    scheduler.ScheduleJob(newTrigger);
                }
            }
        }

        //var scheduler = _schedulerFactory.GetScheduler().Result;
        //var allTriggerKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());
        //foreach (var triggerKey in allTriggerKeys.Result)
        //{
        //    var triggerdetails = scheduler.GetTrigger(triggerKey);
        //    var Jobdetails = scheduler.GetJobDetail(triggerdetails.Result.JobKey);

        //    Console.WriteLine("IsCompleted -" + triggerdetails.IsCompleted + " |  TriggerKey  - " + triggerdetails.Result.Key.Name + " Job key -" + triggerdetails.Result.JobKey.Name);
        //}
        //_optionsDelegate.OnChange((CitiesSet s, string str) => Console.WriteLine("changed"));
    }
}
