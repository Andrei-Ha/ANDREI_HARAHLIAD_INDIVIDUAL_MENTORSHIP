using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Repositories;
using Exadel.Forecast.Models.Interfaces;
using System;

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

        public string WeatherApiKey { private get; set; }

        public string WeatherBitKey { private get; set; }

        public bool DebugInfo { get; set; }

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
