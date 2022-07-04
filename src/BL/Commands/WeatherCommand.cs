using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class WeatherCommand : ICommand
    {
        private readonly IConfiguration _configuration;
        private readonly string _cityNames;
        private readonly int _amountOfDays;

        public WeatherCommand
            (
                string cityNames,
                IConfiguration configuration,
                int amountOfDays
            )
        {
            _configuration = configuration;
            _cityNames = cityNames;
            _amountOfDays = amountOfDays;
        }

        public async Task<List<DebugModel<ForecastModel>>> GetResultAsync()
        {

            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(_configuration.ExecutionTime);
            CancellationToken token = source.Token;

            var forecastRepository = _configuration.GetDefaultForecastApi();
            string[] cityNames = _cityNames.Split(',').Select(p => p.Trim()).ToArray();
            List<Task<DebugModel<ForecastModel>>> tasksList = new List<Task<DebugModel<ForecastModel>>>();

            foreach (var cityName in cityNames)
            {
                tasksList.Add(forecastRepository.GetForecastAsync(cityName, _amountOfDays, token)) ;
            }

            var debugForecastModelList = (await Task.WhenAll(tasksList)).ToList();
            return debugForecastModelList;
        }
    }
}
