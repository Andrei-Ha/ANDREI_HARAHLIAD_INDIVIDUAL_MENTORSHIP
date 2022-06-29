using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.OpenWeather;
using Exadel.Forecast.DAL.Models.Weatherapi;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class WeatherapiRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public WeatherapiRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<DebugModel<CurrentModel>> GetTempByNameAsync(string cityName)
        {
            string webUrl = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            var requestSender = new RequestSender<WeatherapiCurrentModel>(webUrl);
            DebugModel<WeatherapiCurrentModel> openWeatherDebugModel = await requestSender.GetDebugModelAsync();
            var currentDebugModel = new DebugModel<CurrentModel>()
            {
                RequestDuration = openWeatherDebugModel.RequestDuration,
                TextException = openWeatherDebugModel.TextException,
                Model = openWeatherDebugModel.Model.GetCurrentModel()
            };
            return currentDebugModel;
        }

        public Task<CurrentModel[]> GetForecastByNameAsync(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
