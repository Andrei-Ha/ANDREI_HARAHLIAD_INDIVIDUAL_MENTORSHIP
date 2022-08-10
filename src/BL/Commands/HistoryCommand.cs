using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using Exadel.Forecast.Models.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class HistoryCommand : IForecastCommand
    {
        private readonly IConfiguration _configuration;
        private readonly List<string> _cityList;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;
        private readonly WeatherDbContext? _dbContext;

        public HistoryCommand
            (
                string cityNames,
                IConfiguration configuration,
                DateTime startDate,
                DateTime endDate,
                WeatherDbContext? dbContext
            )
        {
            _configuration = configuration;
            _cityList = cityNames.Split(',').Select(p => p.Trim()).ToList();
            _startDate = startDate;
            _endDate = endDate;
            _dbContext = dbContext;
        }

        private async Task<List<DebugModel<ForecastModel>>> GetListFromApi(CancellationToken token)
        {
            var forecastRepository = _configuration.GetDefaultForecastApi();
            List<Task<DebugModel<ForecastModel>>> tasksList = new();

            foreach (var cityName in _cityList)
            {
                tasksList.Add(forecastRepository.GetHistoryAsync(cityName, _startDate, _endDate, token));
            }

            return (await Task.WhenAll(tasksList)).ToList();
        }

        private async Task<List<DebugModel<ForecastModel>>> GetListFromContext(CancellationToken token) 
        {
            List<DebugModel<ForecastModel>> debugModelListFromContext = new();
            
            if (_dbContext != null)
            {
                var models = _dbContext.ForecastModels.Include(p => p.History).AsNoTracking();

                if (_cityList.Count > 0)
                {
                    models = models.Where(m => _cityList.Contains(m.City));
                }

                models = models.Select(p => new ForecastModel()
                {
                    City = p.City,
                    History = p.History.Where(h => h.Date >= _startDate && h.Date <= _endDate).ToList()
                });

                var modelsList = await models.ToListAsync();
                debugModelListFromContext = modelsList.Select(p => new DebugModel<ForecastModel>() { Model = p }).ToList();
            }

            return debugModelListFromContext;
        }

        public async Task<List<DebugModel<ForecastModel>>> GetResultAsync()
        {
            CancellationTokenSource source = new();
            source.CancelAfter(_configuration.ExecutionTime);
            CancellationToken token = source.Token;

            var debugModelListFromApi = await GetListFromApi(token);
            var debugModelListFromContext = await GetListFromContext(token);

            List<DebugModel<ForecastModel>> resultList = new();
            List<CurrentModel> historyList = new();

            foreach (var cityName in _cityList) 
            {
                historyList.Clear();
                var apiModel = debugModelListFromApi?.FirstOrDefault(c => c.Model.City == cityName);

                if (apiModel != null)
                {
                    historyList.AddRange(apiModel.Model.History);
                }

                var contextModel = debugModelListFromContext?.FirstOrDefault(c => c.Model.City == cityName);
                if (contextModel != null)
                {
                    historyList.AddRange(contextModel.Model.History);
                }

                resultList.Add(new DebugModel<ForecastModel>() 
                {
                    Model = new ForecastModel() 
                    { 
                        City = cityName,
                        History = historyList
                            .Where(h => h.Date >= _startDate && h.Date <= _endDate)
                            .Distinct()
                            .OrderBy(d => d.Date)
                            .ToList() 
                    } 
                });
            }

            return resultList;
        }
    }
}
