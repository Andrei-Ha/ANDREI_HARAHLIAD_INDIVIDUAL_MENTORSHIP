using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class ForecastWeatherCommand : ICommand
    {
        private readonly string _cityName;
        private readonly int _forecastDays;
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

        public async Task<string> GetResultAsync()
        {
            if (!_cityValidator.IsValid(_cityName))
            {
                return "You need to type name of the city!";
            }

            var forecastRepository = _configuration.GetDefaultForecastApi();
            ForecastResponseModel[] models = await forecastRepository.GetForecastByNameAsync(_cityName);
            if (models == null)
            {
                return "no data";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{_cityName} weather forecast:");
            int i = 1;
            foreach (var model in models) if (i <= _forecastDays)
            {
                sb.Append($"Day {i++} ({model.Date:dd.MM.yyyy}): ");
                sb.AppendLine(_responseBuilder.WeatherStringByTemp(_cityName, model.Temperature));
            }
            return sb.ToString();
        }
    }
}
