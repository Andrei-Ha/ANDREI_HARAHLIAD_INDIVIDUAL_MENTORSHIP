using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.WeatherBit;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public ForecastResponseModel[] GetForecastByName(string cityName)
        {
            string webUrl = $"https://api.weatherbit.io/v2.0/forecast/daily?key={_apiKey}&city={cityName}";
            var requestSender = new RequestSender<WeatherBitForecastModel>();
            var model = requestSender.GetModel(webUrl).Result;
            if (model != null)
            {
                return model.Data
                    .Select(p => new ForecastResponseModel()
                        { 
                            temperature = p.Temp, date = DateTime.ParseExact(p.Datetime, "yyyy-MM-dd", CultureInfo.InvariantCulture) 
                        })
                    .ToArray();
            }

            return null;
        }

        public double GetTempByName(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
