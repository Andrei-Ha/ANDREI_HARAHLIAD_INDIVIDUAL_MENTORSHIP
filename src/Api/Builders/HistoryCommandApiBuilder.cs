using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Models.Configuration;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public class HistoryCommandApiBuilder : BaseCommandApiBuilder<HistoryCommand>
    {
        private readonly WeatherDbContext? _dbContext;
        private readonly HistoryQueryDTO _queryDTO;
        private DateTime _startDate;
        private DateTime _endDate;

        public HistoryCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator,
            HistoryQueryDTO historyQueryDTO,
            WeatherDbContext? weatherDbContext = default) : base(configuration, cityValidator) 
        {
            _dbContext = weatherDbContext;
            _queryDTO = historyQueryDTO;
        }

        private void SetTimeInterval(DateTime startDate, DateTime endDate)
        {
            var timeIntervalValidator = new TimeIntervalValidator();

            if (!timeIntervalValidator.IsValid(startDate, endDate))
            {
                throw new HttpRequestException("StartDate can't be bigger than EndDate!", null, System.Net.HttpStatusCode.UnprocessableEntity);
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
