using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.WeatherBit;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
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

        public async Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default)
        {
            var forecastModel = new ForecastModel();
            var forecastDebugModel = new DebugModel<ForecastModel>();
            string webUrl = $"https://api.weatherbit.io/v2.0/current?key={_apiKey}&city={cityName}";
            var requestSender = new RequestSender<WeatherBitCurrentModel>(webUrl);
            var weatherBitCurrentDebugModel = await requestSender.GetDebugModelAsync(token);
            forecastDebugModel.RequestDuration = weatherBitCurrentDebugModel.RequestDuration;
            forecastDebugModel.TextException = weatherBitCurrentDebugModel.TextException;
            forecastDebugModel.Model = weatherBitCurrentDebugModel.Model == null ? default : weatherBitCurrentDebugModel.Model.UpdateForecastModel(forecastModel);

            if (amountOfDays > 0)
            {
                webUrl = $"https://api.weatherbit.io/v2.0/forecast/daily?key={_apiKey}&city={cityName}&days={amountOfDays}";
                var newRequestSender = new RequestSender<WeatherBitForecastModel>(webUrl);
                var weatherBitForecastDebugModel = await newRequestSender.GetDebugModelAsync(token);
                forecastDebugModel.RequestDuration += weatherBitForecastDebugModel.RequestDuration;
                forecastDebugModel.TextException += $"{Environment.NewLine}{weatherBitForecastDebugModel.TextException}";
                forecastDebugModel.Model = weatherBitForecastDebugModel.Model == null ? default : weatherBitForecastDebugModel.Model.UpdateForecastModel(forecastDebugModel.Model);
            }

            var result = forecastDebugModel;
            return result;
        }

        public Task<DebugModel<ForecastModel>> GetHistoryAsync(string cityName, DateTime startDate, DateTime endDate, CancellationToken token = default)
        {
            throw new NotImplementedException();
        }
    }
}
