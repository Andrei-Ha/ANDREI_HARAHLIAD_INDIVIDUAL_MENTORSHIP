using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Commands
{
    public class ForecastWeatherCommand : ICommand
    {
        private string _cityName;
        private int _forecastDays;
        private readonly IConfiguration _configuration;
        private readonly IValidator<string> _cityValidator;
        private readonly IResponseBuilder _responseBuilder;
        public ForecastWeatherCommand(string cityName, int forecastDays, IConfiguration configuration, IValidator<string> cityValidator, IResponseBuilder responseBuilder)
        {
            _cityName = cityName;
            _forecastDays = forecastDays;
            _configuration = configuration;
            _cityValidator = cityValidator;
            _responseBuilder = responseBuilder;
        }

        public string GetResult()
        {
            if (!_cityValidator.IsValid(_cityName))
            {
                return "You need to type name of the city!";
            }

            var forecastRepository = _configuration.GetDefaultForecastApi();
            double[] temps = forecastRepository.GetForecastByName(_cityName);
            if (temps == null)
            {
                return "no data";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{_cityName} weather forecast:");
            int i = 1;
            foreach (var temp in temps) if (i <= _forecastDays)
            {
                sb.Append($"Day {i++}: ");
                sb.AppendLine(_responseBuilder.WeatherStringByTemp(_cityName, temp));
            }
            return sb.ToString();
        }
    }
}
