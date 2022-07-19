using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public class CurrentCommandApiBuilder : BaseCommandBuilder<WeatherCommand>
    {
        private readonly CurrentQueryDTO _queryDTO;

        public CurrentCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator,
            CurrentQueryDTO queryDTO) : base(configuration, cityValidator)
        {
            _queryDTO = queryDTO;
        }
        public override Task<WeatherCommand> BuildCommand()
        {
            SetWeatherProvider(_queryDTO.ForecastApi);
            SetCityName(string.Join(",", _queryDTO.Cities));
            return Task.FromResult(new WeatherCommand(CityName, Configuration, 0));
        }
    }
}
