using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models.OpenWeather;
using System;
using System.Collections.Generic;
using System.Text;
//using Exadel.Forecast.Models

namespace Exadel.Forecast.DAL.Repositories
{
    public class OpenWeatherRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public OpenWeatherRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        /// <summary>
        /// Method return the temperature by the city name
        /// </summary>
        /// <param name="cityName">name of city</param>
        /// <returns>Returns -273 if no result</returns>
        public double GetTempByName(string cityName)
        {
            string webUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={_apiKey}&units=metric";
            var requestSender = new RequestSender<OpenWeatherDayModel>();
            var model = requestSender.GetModel(webUrl).Result;
            if (model != null)
            {
                return model.Main.Temp;
            }

            return -273;
        }
    }
}
