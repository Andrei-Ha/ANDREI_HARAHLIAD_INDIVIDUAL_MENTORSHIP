﻿using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp.CommandBuilders
{
    public class ForecastCommandCmdBuilder : BaseCommandCmdBuilder
    {
        protected readonly IValidator<int> _forecastNumberValidator;

        public ForecastCommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> validator,
            IValidator<int> forecastNumberValidator) : base(configuration, validator)
        {
            _forecastNumberValidator = forecastNumberValidator;
        }

        public void SetNumberOfForecastDaysByUser()
        {
            Console.WriteLine($"Set the number of forecast days" +
                $" between {Configuration.MinAmountOfDays} and {Configuration.MaxAmountOfDays}!");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int value) && _forecastNumberValidator.IsValid(value))
            {
                AmountOfDays = value;
            }
            else
            {
                Console.WriteLine("Wrong number!");
                SetNumberOfForecastDaysByUser();
            }
        }

        public override Task<WeatherCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.WeatherBit);
            SetNumberOfForecastDaysByUser();
            SetCityNameByUser();
            return Task.FromResult(new WeatherCommand(CityName, Configuration, AmountOfDays));
        }
    }
}
