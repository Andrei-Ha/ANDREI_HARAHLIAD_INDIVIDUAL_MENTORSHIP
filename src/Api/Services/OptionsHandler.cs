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
            _citiesSet = _optionsMonitor.CurrentValue;
            _schedulerFactory = schedulerFactory;
        }

        private void UpdateTriggers(CitiesSet citiesSet, string str)
        {
            Console.WriteLine("UpdateTrigger");
            var scheduler = _schedulerFactory.GetScheduler().Result;
            var jobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.GroupEquals("WEATHER")).Result;

            if (_citiesSet.GetAllTimerAsString() != citiesSet.GetAllTimerAsString())
            {
                Console.WriteLine(" --- ConfigChanged");
                _citiesSet = citiesSet;
                foreach (var job in jobKeys)
                {
                    scheduler.DeleteJob(job);
                }

                AddSavingWeatherJobToScheduler();

                var allJobKeys = scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
                foreach (var jobKey in allJobKeys.Result)
                {
                    Console.WriteLine($"jobKey: {jobKey}");
                }
            }
        }

        public void AddSavingWeatherJobToScheduler()
        {
            Console.WriteLine("in addSavingWeatherJobToScheduler");
            IJobDetail jobDetail = JobBuilder.Create<SavingWeatherJob>()
                .WithIdentity(nameof(SavingWeatherJob), "WEATHER")
                .Build();

            List<ITrigger> list = new();
            ITrigger trigger;
            foreach (int timer in _optionsMonitor.CurrentValue.Cities.Select(c => c.Timer).OrderBy(c => c).Distinct())
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{nameof(SavingWeatherJob)}-Trigger_{timer}", "WEATHER")
                    .ForJob(jobDetail)
                    .StartNow()
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(timer).RepeatForever())
                    .Build();
                list.Add(trigger);
                Console.WriteLine($"Job: {jobDetail.Key.Name}. Added trigger: {trigger.Key.Name} ");
            }

            var scheduler = _schedulerFactory.GetScheduler().Result;
            scheduler.ScheduleJob(jobDetail, list, true);
        }
    }
}
