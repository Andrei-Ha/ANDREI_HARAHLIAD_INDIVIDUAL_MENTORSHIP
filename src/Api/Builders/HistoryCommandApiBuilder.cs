using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Models;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Models.Configuration;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public class HistoryCommandApiBuilder : BaseCommandApiBuilder<HistoryCommand>
    {
        private const string wrongTimeInterval = "StartDate and EndDate are required and StartDate can't be bigger than EndDate!";
        private readonly WeatherDbContext? _dbContext;
        private readonly HistoryQueryDTO _queryDTO;
        private readonly IValidator<TimeInterval> _timeIntervalValidator;
        private DateTime _startDate;
        private DateTime _endDate;

        public HistoryCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator,
            HistoryQueryDTO historyQueryDTO,
            IValidator<TimeInterval> timeIntervalValidator,
            WeatherDbContext? weatherDbContext = default) : base(configuration, cityValidator) 
        {
            _dbContext = weatherDbContext;
            _queryDTO = historyQueryDTO;
            _timeIntervalValidator = timeIntervalValidator;
        }

        private void SetTimeInterval(DateTime startDate, DateTime endDate)
        {
            if (!_timeIntervalValidator.IsValid(new() { StartDateTime = startDate, EndDateTime = endDate }))
            {
                throw new HttpRequestException(wrongTimeInterval, null, System.Net.HttpStatusCode.UnprocessableEntity);
            }
            else
            {
                _startDate = startDate;
                _endDate = endDate;
            }
        }

        public override Task<HistoryCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.WeatherApi);
            SetCityName(string.Join(",", _queryDTO.Cities));
            SetTimeInterval(_queryDTO.StartDateTime, _queryDTO.EndDateTime);
            return Task.FromResult(
                new HistoryCommand(CityName, Configuration, _startDate, _endDate, _dbContext));
        }
    }
}
