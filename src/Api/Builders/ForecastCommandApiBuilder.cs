using Exadel.Forecast.Api.DTO;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;


namespace Exadel.Forecast.Api.Builders
{
    public class ForecastCommandApiBuilder : BaseCommandBuilder
    {
        private readonly ForecastQueryDTO _queryDTO;
        private readonly IValidator<int> _forecastNumberValidator;

        public ForecastCommandApiBuilder(
            ModelsConfiguration.IConfiguration configuration,
            IValidator<string> cityValidator,
            ForecastQueryDTO queryDTO,
            IValidator<int> forecastNumberValidator) : base(configuration, cityValidator)
        {
            _queryDTO = queryDTO;
            _forecastNumberValidator = forecastNumberValidator;
        }

        private void SetNumberOfForecastDays(int amountOfDays) 
        {
            if (!_forecastNumberValidator.IsValid(_amountOfDays))
            {
                // Loger: wrong number
                _amountOfDays = Configuration.MinAmountOfDays;
            }
            else
            {
                _amountOfDays = amountOfDays;
            }
        }


        public override Task<WeatherCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.WeatherBit);
            SetCityName(string.Join(",", _queryDTO.Cities));
            SetNumberOfForecastDays(_queryDTO.Days);
            return Task.FromResult(new WeatherCommand(_cityName, Configuration, _amountOfDays));
        }
    }
}
