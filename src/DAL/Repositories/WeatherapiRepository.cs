using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Models.Weatherapi;
using Exadel.Forecast.DAL.Services;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
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

        private async Task<DebugModel<ForecastModel>> Foo(string webUrl, CancellationToken token)
        {
            var forecastModel = new ForecastModel();
            var forecastDebugModel = new DebugModel<ForecastModel>();
            var requestSender = new RequestSender<WeatherapiForecastModel>(webUrl);
            DebugModel<WeatherapiForecastModel> weatherapiDebugModel = await requestSender.GetDebugModelAsync(token);
            forecastDebugModel.RequestDuration = weatherapiDebugModel.RequestDuration;
            forecastDebugModel.TextException = weatherapiDebugModel.TextException;
            forecastDebugModel.Model = weatherapiDebugModel.Model == null ? default : weatherapiDebugModel.Model.UpdateForecastModel(forecastModel);

            return forecastDebugModel;
        }

        public async Task<DebugModel<ForecastModel>> GetForecastAsync(string cityName, int amountOfDays, CancellationToken token = default)
        {
            string webUrl = 
                $"https://api.weatherapi.com/v1/forecast.json?key={_apiKey}&q={cityName}&days={amountOfDays}&aqi=no&alerts=no&hour=no";

            return await Foo(webUrl, token);
        }

        public async Task<DebugModel<ForecastModel>> GetHistoryAsync(
            string cityName, DateTime startDate, DateTime endDate, CancellationToken token = default)
        {
            List<Task<DebugModel<ForecastModel>>> tasksList = new();
            startDate = startDate.Date >= DateTime.Now.Date.AddDays(-5) ? startDate : DateTime.Now.Date.AddDays(-5);
            endDate = endDate.Date < DateTime.Now.Date ? endDate : DateTime.Now.Date;

            while (startDate.Date <= endDate.Date)
            {
                tasksList.Add(Foo($"https://api.weatherapi.com/v1/history.json?key={_apiKey}&q={cityName}&dt={endDate:yyyy-MM-dd}", token));
                endDate = endDate.AddDays(-1);
            }

            var debugForecastModelList = await Task.WhenAll(tasksList);
            var resultDebugModel = new DebugModel<ForecastModel>() { Model = new ForecastModel()};

            foreach (var debugModel in debugForecastModelList)
            {
                resultDebugModel.TextException += debugModel.TextException + Environment.NewLine;

                resultDebugModel.RequestDuration = resultDebugModel.RequestDuration < debugModel.RequestDuration ? 
                    debugModel.RequestDuration : resultDebugModel.RequestDuration;
                if (debugModel.Model != null)
                {
                    resultDebugModel.Model.City = string.IsNullOrEmpty(resultDebugModel.Model.City) ?
                        debugModel.Model.City : resultDebugModel.Model.City;

                    resultDebugModel.Model.History.AddRange(debugModel.Model.History);
                }
            }

            return resultDebugModel;
        }
    }
}
