using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Repositories;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;

namespace Exadel.Forecast.Models.Configuration
{
    public class Configuration : IConfiguration
    {
        private IWebApiRepository _defaultRepository;
        private string _openWeatherKey;        

        public string OpenWeatherKey { 
            set
            {
                _openWeatherKey = value;
            } 
        }

        public int MinAmountOfDays { get; set; }

        public int MaxAmountOfDays { get; set; }

        public string WeatherApiKey { private get; set; }

        public string WeatherBitKey { private get; set; }

        public bool DebugInfo { get; set; }

        public int ExecutionTime { get; set; }

        public List<int> ReportsIntervals { get; set; }

        public string UsersEndpointUrl { get; set; }

        public void SetDefaultForecastApi(ForecastApi forecastApi)
        {
            switch (forecastApi)
            {
                case ForecastApi.OpenWeather:
                    _defaultRepository = new OpenWeatherRepository(_openWeatherKey);
                    break;
                case ForecastApi.WeatherApi:
                    _defaultRepository = new WeatherapiRepository(WeatherApiKey);
                    break;
                case ForecastApi.WeatherBit:
                    _defaultRepository = new WeatherBitRepository(WeatherBitKey);
                    break;
                default:
                    break;
            }
        }

        public IWebApiRepository GetDefaultForecastApi() => _defaultRepository;

    }
}
