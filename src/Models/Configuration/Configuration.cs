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

        public string WeatherApiKey
        {
            set; private get;
        }

        public void SetDefaultForecastApi(ForecastApi forecastApi)
        {
            if (forecastApi == ForecastApi.OpenWeather)
            {
                _defaultRepository = new OpenWeatherRepository(_openWeatherKey);
            }
            else if(forecastApi == ForecastApi.WeatherApi)
            {
                _defaultRepository = new WeatherapiRepository(WeatherApiKey);
            }
        }

        public IWebApiRepository GetDefaultForecastApi() => _defaultRepository;
    }
}
