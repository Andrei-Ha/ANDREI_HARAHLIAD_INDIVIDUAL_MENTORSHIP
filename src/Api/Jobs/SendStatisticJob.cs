using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System.Text;

namespace Exadel.Forecast.Api.Jobs
{
    public class SendStatisticJob : IJob
    {
        private readonly ILogger<SendStatisticJob> _logger;
        public readonly WeatherDbContext _weatherDbContext;
        private readonly IWeatherService<WeatherHistoryDTO, HistoryQueryDTO> _historyService;

        public SendStatisticJob(
            ILogger<SendStatisticJob> logger,
            WeatherDbContext weatherDbContext,
            IWeatherService<WeatherHistoryDTO,
            HistoryQueryDTO> historyService)
        {
            _logger = logger;
            _weatherDbContext = weatherDbContext;
            _historyService = historyService;
        }

        private async Task<string> GetReport(SubscriptionModel model)
        {
            StringBuilder sb = new();
            sb.AppendLine($"The report was generated: {DateTime.Now:dd.MM.yyyy HH:mm:ss}. Period: for the last {model.Hours} hours");
            var historyQuery = new HistoryQueryDTO()
            {
                Cities = model.Cities,
                StartDateTime = DateTime.Now.AddHours(-model.Hours),
                EndDateTime = DateTime.Now,
            };
            var weatherHistoryDTOs = await _historyService.Get(historyQuery);

            if(weatherHistoryDTOs != null && weatherHistoryDTOs.Any())
            {
                double avgTemerature = default;
                foreach (var weatherHistoryDTO in weatherHistoryDTOs)
                {
                    if (weatherHistoryDTO.History.Any())
                    {
                        avgTemerature = weatherHistoryDTO.History.Average(t => t.Temperature);
                        sb.AppendLine($"{weatherHistoryDTO.City} average temperature: {string.Format("{0:F1}", avgTemerature)} °C.");
                    }
                    else
                    {
                        sb.AppendLine($"{weatherHistoryDTO.City}: no statistics.");
                    }
                }
            }

            return sb.ToString();
        }

        private async Task SendReports(int hours)
        {
            var subscriptions = await _weatherDbContext.SubscriptionModels.Where(p => p.Hours == hours).ToListAsync();
            string report = string.Empty;
            if (subscriptions.Any())
            {
                foreach (var subscription in subscriptions)
                {
                    report = await GetReport(subscription);
                    // todo In this place we need to use the Sender service!
                    Console.WriteLine($"Send report to {subscription.Email} {Environment.NewLine}" + report);
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
                await SendReports(hours);
            }
        }
    }
}
