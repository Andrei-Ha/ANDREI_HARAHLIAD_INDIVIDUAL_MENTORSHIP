using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.Weatherapi;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
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

        public async Task<DebugModel<CurrentModel>> GetCurrentWeatherAsync(string cityName)
        {
            string webUrl = $"http://api.weatherapi.com/v1/current.json?key={_apiKey}&q={cityName}";
            var requestSender = new RequestSender<WeatherapiCurrentModel>(webUrl);
            DebugModel<WeatherapiCurrentModel> weatherapiDebugModel = await requestSender.GetDebugModelAsync();
            var currentDebugModel = new DebugModel<CurrentModel>()
            {
                RequestDuration = weatherapiDebugModel.RequestDuration,
                TextException = weatherapiDebugModel.TextException,
                Model = weatherapiDebugModel.Model == null ? default : weatherapiDebugModel.Model.GetCurrentModel()
            };
            return currentDebugModel;
        }

        public Task<ForecastModel> GetWeatherForecastAsync(string cityName, int amountOfDays)
        {
            throw new NotImplementedException();
        }
    }
}
