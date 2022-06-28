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
        private readonly IValidator<string> _cityValidator;
        private readonly IValidator<double> _temperatureValidator;
        private readonly IResponseBuilder _responseBuilder;
        private readonly string _cityName;

        public CurrentWeatherCommand
            (
                string cityName,
                IConfiguration configuration,
                IValidator<string> cityValidator,
                IValidator<double> temperatureValidator,
                IResponseBuilder responseBuilder
            )
        {
            _configuration = configuration;
            _cityValidator = cityValidator;
            _temperatureValidator = temperatureValidator;
            _responseBuilder = responseBuilder;
            _cityName = cityName;
        }

        public async Task<string> GetResultAsync()
        {
            if (!_cityValidator.IsValid(_cityName))
            {
                return "An invalid city name was entered!";
            }

            var forecastRepository = _configuration.GetDefaultForecastApi();
            var response = await forecastRepository.GetTempByNameAsync(_cityName);
            var temperature = response.Temperature;

            if (!_temperatureValidator.IsValid(temperature))
            {
                return $"There is no forecast for your city!{Environment.NewLine}Exception:{response.TextException}";
            }

            return _responseBuilder.WeatherStringByTemp(_cityName, temperature);
        }
    }
}
