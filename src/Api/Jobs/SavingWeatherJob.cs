using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    [DisallowConcurrentExecution]
    public class SavingWeatherJob : IJob
    {
        private readonly ILogger<SavingWeatherJob> _logger;
        public SavingWeatherJob(ILogger<SavingWeatherJob> logger) 
        {
            _logger = logger; 
        }
        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation($"Hello World! Time: {DateTime.Now.ToLongTimeString()}");
            return Task.CompletedTask;
        }
    }
}
