using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Services;
using Exadel.Forecast.Domain.Models;
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
        private readonly int _amountOfDays;
        private readonly IConfiguration _configuration;
        private readonly IResponseBuilder _responseBuilder;

        public ForecastWeatherCommand
            (
                string cityName,
                int amountOfDays,
                IConfiguration configuration,
                IResponseBuilder responseBuilder
            )
        {
            _cityName = cityName;
            _amountOfDays = amountOfDays;
            _configuration = configuration;
            _responseBuilder = responseBuilder;
        }

        public async Task<string> GetResultAsync()
        {
            var forecastRepository = _configuration.GetDefaultForecastApi();
            ForecastModel forecastModel = await forecastRepository.GetWeatherForecastAsync(_cityName, _amountOfDays);

            if (forecastModel == null)
            {
                return "no data";
            }

            return _responseBuilder.BuildForecast(forecastModel);
        }
    }
}
