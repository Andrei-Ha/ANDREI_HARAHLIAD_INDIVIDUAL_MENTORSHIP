using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models.WeatherBit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherBitRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public WeatherBitRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        double[] IWebApiRepository.GetForecastByName(string cityName)
        {
            string webUrl = $"https://api.weatherbit.io/v2.0/forecast/daily?key={_apiKey}&city={cityName}";
            var requestSender = new RequestSender<WeatherBitForecastModel>();
            var model = requestSender.GetModel(webUrl).Result;
            if (model != null)
            {
                return model.Data.Select(p => p.Temp).ToArray();
            }

            return null;
        }

        double IWebApiRepository.GetTempByName(string cityName)
        {
            return -273;
        }
    }
}
