using Exadel.Forecast.Api.Services;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    public class CreatingTimerTriggersJob : IJob
    {
        private readonly OptionsHandler _optionsHandler;
        private readonly ILogger<CreatingTimerTriggersJob> _logger;

        public CreatingTimerTriggersJob(OptionsHandler optionsHandler, ILogger<CreatingTimerTriggersJob> logger)
        {
            _optionsHandler = optionsHandler;
            _logger = logger;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("CreatingTimerTriggersJob _ Executed");
            _optionsHandler.AddSavingWeatherJobToScheduler();
            return Task.CompletedTask;
        }
    }
}
