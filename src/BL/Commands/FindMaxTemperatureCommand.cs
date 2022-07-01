using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class FindMaxTemperatureCommand : ICommand
    {
        private readonly IConfiguration _configuration;
        private readonly string _cityNames;
        private readonly IResponseBuilder _responseBuilder;

        public FindMaxTemperatureCommand
            (
                string cityNames,
                IConfiguration configuration,
                IResponseBuilder responseBuilder
            )
        {
            _configuration = configuration;
            _cityNames = cityNames;
            _responseBuilder = responseBuilder;
        }

        public async Task<string> GetResultAsync()
        {
            var forecastRepository = _configuration.GetDefaultForecastApi();
            string[] cityNames = _cityNames.Split(',').Select(p => p.Trim()).ToArray();
            List<Task<DebugModel<CurrentModel>>> tasksList = new List<Task<DebugModel<CurrentModel>>>();

            foreach (var cityName in cityNames)
            {
                tasksList.Add(forecastRepository.GetCurrentWeatherAsync(cityName));
            }

            var debugCurrentModelList = (await Task.WhenAll(tasksList)).ToList();
            return _responseBuilder.BuildMaxCurrent(debugCurrentModelList, _configuration.DebugInfo);
        }
    }
}
