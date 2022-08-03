using Exadel.Forecast.Api.Jobs;
using Quartz;

namespace Exadel.Forecast.Api.Services
{
    public class SubscriptionHandler
    {
        private readonly ISchedulerFactory _schedulerFactory;
        private readonly ILogger<SubscriptionHandler> _logger;

        public SubscriptionHandler(
            ISchedulerFactory schedulerFactory,
            ILogger<SubscriptionHandler> logger)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task AddTriggersToSendStatisticJobToSchedulerAsync()
        {
            _logger.LogInformation("Method AddAllSendStatisticJobToScheduler started executing");
            IJobDetail jobDetail = JobBuilder.Create<SendStatisticJob>()
                .WithIdentity(nameof(SavingWeatherJob), "STATISTIC")
                .Build();

            List<ITrigger> list = new();
            ITrigger trigger;

            // todo Intervals can be moved to the config
            var intervals = new List<int>() { 1, 3, 12, 24};

            foreach (int hours in intervals)
            {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"{nameof(SendStatisticJob)}-Trigger_{hours}", "STATISTIC")
                    .ForJob(jobDetail)
                    .StartAt(default)
                    //.WithSimpleSchedule(s => s.WithIntervalInHours(hours).RepeatForever())
                    .WithSimpleSchedule(s => s.WithIntervalInSeconds(hours).RepeatForever())
                    .Build();
                list.Add(trigger);
                _logger.LogInformation($"Job: {jobDetail.Key.Name}. Added trigger: {trigger.Key.Name} ");
            }

            var scheduler = await _schedulerFactory.GetScheduler();
            _ = scheduler.ScheduleJob(jobDetail, list, true);
        }
    }
}
