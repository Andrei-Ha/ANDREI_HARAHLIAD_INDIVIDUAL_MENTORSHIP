using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.OpenWeather;
using Exadel.Forecast.DAL.Services;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class OpenWeatherRepository : IWebApiRepository
    {
        private readonly string _apiKey;

        public OpenWeatherRepository(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<DebugModel<CurrentModel>> GetTempByNameAsync(string cityName)
        {
            string webUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={_apiKey}&units=metric";
            var requestSender = new RequestSender<OpenWeatherCurrentModel>(webUrl);
            DebugModel<OpenWeatherCurrentModel> openWeatherDebugModel = await requestSender.GetDebugModelAsync();
            var currentDebugModel = new DebugModel<CurrentModel>() {
                RequestDuration = openWeatherDebugModel.RequestDuration,
                TextException = openWeatherDebugModel.TextException,
                Model = openWeatherDebugModel.Model.GetCurrentModel()};
            return currentDebugModel;
        }

        public Task<CurrentModel[]> GetForecastByNameAsync(string cityName)
        {
            throw new NotImplementedException();
        }
    }
}
