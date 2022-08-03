using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    public class SendStatisticJob : IJob
    {
        private readonly ILogger<SendStatisticJob> _logger;
        private readonly SubscriptionDbContext _subscriptionDbContext;
        public readonly WeatherDbContext _weatherDbContext;
        private readonly IWeatherService<WeatherHistoryDTO, HistoryQueryDTO> _historyService;

        public SendStatisticJob(
            ILogger<SendStatisticJob> logger,
            SubscriptionDbContext subscriptionDbContext,
            WeatherDbContext weatherDbContext,
            IWeatherService<WeatherHistoryDTO,
            HistoryQueryDTO> historyService)
        {
            _logger = logger;
            _subscriptionDbContext = subscriptionDbContext;
            _weatherDbContext = weatherDbContext;
            _historyService = historyService;
        }

        private async Task Func(int hours)
        {
            var subscriptions = await _subscriptionDbContext.SubscriptionModels.Where(p => p.Hours == hours).ToListAsync();

            if (subscriptions.Any())
            {
                foreach (var subscription in subscriptions)
                {
                    Console.WriteLine($"sent report to {subscription.Email}, hours: {subscription.Hours}");
                }
            }

        }

        public async Task Execute(IJobExecutionContext context)
        {
            string triggerKey = context.Trigger.Key.Name;
            _logger.LogInformation($"Statistic job started for {triggerKey}. time: {DateTime.Now}");
            var arr = triggerKey.Split('_');

            if (arr.Length > 1 && int.TryParse(arr[1], out int hours))
            {
                await Func(hours);
            }
            

        }
    }
}
