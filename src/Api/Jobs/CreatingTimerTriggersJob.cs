using Exadel.Forecast.Api.Services;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    public class CreatingTimerTriggersJob : IJob
    {
        private readonly OptionsHandler _optionsHandler;

        public CreatingTimerTriggersJob(OptionsHandler optionsHandler)
        {
            _optionsHandler = optionsHandler;
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("CreatingTimerTriggersJob _ Executed");
            _optionsHandler.AddSavingWeatherJobToScheduler();
            return Task.CompletedTask;
        }
    }
}
