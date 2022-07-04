using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.Weatherapi;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
using System.Threading;
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

        public async Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default)
        {
            var forecastModel = new ForecastModel();
            var forecastDebugModel = new DebugModel<ForecastModel>();
            string webUrl = $"https://api.weatherapi.com/v1/forecast.json?key={_apiKey}&q={cityName}&days={amountOfDays}&aqi=no&alerts=no&hour=no";
            var requestSender = new RequestSender<WeatherapiForecastModel>(webUrl);
            DebugModel<WeatherapiForecastModel> weatherapiDebugModel = await requestSender.GetDebugModelAsync(token);
            forecastDebugModel.RequestDuration = weatherapiDebugModel.RequestDuration;
            forecastDebugModel.TextException = weatherapiDebugModel.TextException;
            forecastDebugModel.Model = weatherapiDebugModel.Model == null ? default : weatherapiDebugModel.Model.UpdateForecastModel(forecastModel);

            return forecastDebugModel;
        }
    }
}
