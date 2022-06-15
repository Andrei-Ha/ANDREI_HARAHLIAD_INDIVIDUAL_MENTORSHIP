using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
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
            WeatherapiDayModel model = RequestSender<WeatherapiDayModel>.GetModel(webUrl).Result;
            if (model != null)
            {
                return model.current.temp_c;
            }

            return -273;
        }
    }
}
