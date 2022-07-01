using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Services;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public class ForecastCommandCmdBuilder : BaseCommandCmdBuilder<ForecastWeatherCommand>
    {
        private readonly IValidator<int> _forecastNumberValidator;
        private int _amountOfDays;

        public ForecastCommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator,
            IValidator<int> forecastNumberValidator) : base(configuration, cityValidator)
        {
            _forecastNumberValidator = forecastNumberValidator;
        }

        public void SetNumberOfForecastDays()
        {
            Console.WriteLine($"Set the number of forecast days" +
                $" between {Configuration.MaxAmountOfDays} and {Configuration.MinAmountOfDays}!");
            
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && _forecastNumberValidator.IsValid(value))
            {
                _amountOfDays = value;
            }
            else
            {
                Console.WriteLine("Wrong number!");
                SetNumberOfForecastDays();
            }
        }

        public override async Task<ForecastWeatherCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.WeatherBit);
            SetNumberOfForecastDays();
            SetCityNameFromUser();

            return new ForecastWeatherCommand
                (_cityName, _amountOfDays, Configuration, new ResponseBuilder(new TemperatureValidator()));
        }
    }
}
