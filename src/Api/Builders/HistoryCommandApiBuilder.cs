using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Models.Configuration;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public class HistoryCommandApiBuilder : BaseCommandBuilder<HistoryCommand>
    {
        private readonly WeatherDbContext? _dbContext;
        private readonly HistoryQueryDTO _queryDTO;

        public HistoryCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator,
            HistoryQueryDTO historyQueryDTO,
            WeatherDbContext? weatherDbContext = default) : base(configuration, cityValidator) 
        {
            _dbContext = weatherDbContext;
            _queryDTO = historyQueryDTO;
        }

        public override Task<HistoryCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.WeatherApi);
            SetCityName(string.Join(",", _queryDTO.Cities));
            return Task.FromResult(
                new HistoryCommand(_cityName, Configuration, _queryDTO.StartDateTime, _queryDTO.EndDateTime, _dbContext));
        }
    }
}
