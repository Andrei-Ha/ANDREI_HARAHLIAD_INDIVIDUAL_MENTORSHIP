using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models.Weatherapi;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherapiRepository : IWebApiRepository
    {
        private readonly string _apiKey;
        public WeatherapiRepository(string apiKey)
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
            string webUrl = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            var requestSender = new RequestSender<WeatherapiDayModel>();
            var model = requestSender.GetModel(webUrl).Result;
            if (model != null)
            {
                return model.Current.Temp_c;
            }

            return -273;
        }
    }
}
