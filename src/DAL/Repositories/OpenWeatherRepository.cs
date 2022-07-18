using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.OpenWeather;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
using System.Diagnostics;
using System.Threading;
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

        public async Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default) 
        {
            var forecastModel = new ForecastModel();
            var forecastDebugModel = new DebugModel<ForecastModel>();
            string webUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&APPID={_apiKey}&units=metric";
            var requestSender = new RequestSender<OpenWeatherCurrentModel>(webUrl);
            DebugModel<OpenWeatherCurrentModel> openWeatherDebugModel = await requestSender.GetDebugModelAsync(token);
            forecastDebugModel.RequestDuration = openWeatherDebugModel.RequestDuration;
            forecastDebugModel.TextException = openWeatherDebugModel.TextException;
            forecastDebugModel.Model = openWeatherDebugModel.Model == null ? default : openWeatherDebugModel.Model.UpdateForecastModel(forecastModel);

            return forecastDebugModel;
        }

        public Task<DebugModel<ForecastModel>> GetHistoryAsync(string cityName, DateTime startDate, DateTime endDate, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
