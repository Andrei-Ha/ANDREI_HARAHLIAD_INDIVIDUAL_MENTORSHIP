using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class CurrentWeatherCommand : ICommand
    {
        private readonly IConfiguration _configuration;
        private readonly IResponseBuilder _responseBuilder;
        private readonly string _cityName;

        public CurrentWeatherCommand
            (
                string cityName,
                IConfiguration configuration,
                IResponseBuilder responseBuilder
            )
        {
            _configuration = configuration;
            _responseBuilder = responseBuilder;
            _cityName = cityName;
        }

        public async Task<string> GetResultAsync()
        {
            var forecastRepository = _configuration.GetDefaultForecastApi();
            var debugModel = await forecastRepository.GetCurrentWeatherAsync(_cityName);

            return _responseBuilder.BuildCurrent(debugModel, _configuration.DebugInfo);
        }
    }
}
