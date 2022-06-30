using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.WeatherBit;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherBitRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public WeatherBitRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<ForecastModel> GetWeatherForecastAsync(string cityName)
        {
            string webUrl = $"https://api.weatherbit.io/v2.0/forecast/daily?key={_apiKey}&city={cityName}";
            var requestSender = new RequestSender<WeatherBitForecastModel>(webUrl);
            var model = await requestSender.GetModelAsync();
            if (model != null)
            {
                return model.GetForecastModel();
            }

            return null;
        }

        public Task<DebugModel<CurrentModel>> GetCurrentWeatherAsync(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
