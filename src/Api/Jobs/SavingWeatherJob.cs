using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.Api.Models;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using Microsoft.Extensions.Options;
using Quartz;

namespace Exadel.Forecast.Api.Jobs
{
    [DisallowConcurrentExecution]
    public class SavingWeatherJob : IJob
    {
        private readonly ILogger<SavingWeatherJob> _logger;
        private readonly IOptionsMonitor<CitiesSet> _optionsMonitor;
        private readonly WeatherDbContext _dbContext;
        private readonly ICurrentService _currentService;

        public SavingWeatherJob(
            ILogger<SavingWeatherJob> logger,
            IOptionsMonitor<CitiesSet> optionsMonitor,
            WeatherDbContext weatherDbContext,
            ICurrentService currentService) 
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
            _dbContext = weatherDbContext;
            _currentService = currentService;
        }

        private async Task Func(int timer)
        {
            var cityList = _optionsMonitor.CurrentValue.Cities.Where(c => c.Timer == timer).ToList();
            var cityNameList = cityList.Select(c => c.Name).ToList();

            var currentWeatherDTOList = await _currentService.GetCurrent(new CurrentQueryDTO() { Cities = cityNameList });

            foreach (var currentWeather in currentWeatherDTOList) if (currentWeather != null)
            {
                var model = _dbContext.ForecastModels.FirstOrDefault(m => m.City == currentWeather.City);

                if (model == null)
                {
                    model = new ForecastModel() { City = currentWeather.City };
                    _dbContext.ForecastModels.Add(model);
                }

                model.History.Add(currentWeather.Current);

                _logger.LogInformation($"City: {currentWeather.City} Time: {DateTime.Now.ToLongTimeString()}, Timer: {timer}");
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            string triggerKey = context.Trigger.Key.Name;
            var arr = triggerKey.Split('_');

            if (arr.Length > 1 && int.TryParse(arr[1], out int timer))
            {
                await Func(timer);
            }
        }
    }
}
