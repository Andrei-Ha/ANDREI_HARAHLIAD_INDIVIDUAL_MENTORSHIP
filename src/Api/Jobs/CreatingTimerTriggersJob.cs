using Exadel.Forecast.Api.Services;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    public class CreatingTimerTriggersJob : IJob
    {
        private readonly OptionsHandler _optionsHandler;
        private readonly SubscriptionHandler _subscriptionHandler;
        private readonly ILogger<CreatingTimerTriggersJob> _logger;

        public CreatingTimerTriggersJob(
            OptionsHandler optionsHandler,
            ILogger<CreatingTimerTriggersJob> logger,
            SubscriptionHandler subscriptionHandler)
        {
            _optionsHandler = optionsHandler;
            _logger = logger;
            _subscriptionHandler = subscriptionHandler;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("CreatingTimerTriggersJob _ Executed");
            _optionsHandler.AddSavingWeatherJobToScheduler();
            await _subscriptionHandler.AddTriggersToSendStatisticJobToSchedulerAsync();
        }
    }
}
