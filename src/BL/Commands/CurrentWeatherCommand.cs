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
    public class CurrentWeatherCommand : ICommand<List<DebugModel<CurrentModel>>>
    {
        private readonly IConfiguration _configuration;
        private readonly string _cityNames;

        public CurrentWeatherCommand
            (
                string cityNames,
                IConfiguration configuration
            )
        {
            _configuration = configuration;
            _cityNames = cityNames;
        }

        public async Task<List<DebugModel<CurrentModel>>> GetResultAsync()
        {

            CancellationTokenSource source = new CancellationTokenSource();
            source.CancelAfter(130);
            CancellationToken token = source.Token;

            var forecastRepository = _configuration.GetDefaultForecastApi();
            string[] cityNames = _cityNames.Split(',').Select(p => p.Trim()).ToArray();
            List<Task<DebugModel<CurrentModel>>> tasksList = new List<Task<DebugModel<CurrentModel>>>();

            foreach (var cityName in cityNames)
            {
                tasksList.Add(forecastRepository.GetCurrentWeatherAsync(cityName, token));
            }

            var debugCurrentModelList = (await Task.WhenAll(tasksList)).ToList();
            return debugCurrentModelList;
        }
    }
}
