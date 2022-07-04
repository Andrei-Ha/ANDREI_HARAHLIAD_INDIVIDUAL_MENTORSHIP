using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{

    public class CommandCmdBuilder : BaseCommandBuilder
    {
        public CommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> validator,
            IValidator<int> forecastNumberValidator) : base(configuration, validator, forecastNumberValidator)
        {
        }

        public override void SetWeatherProviderByUser()
        {
            Console.WriteLine($"PLease choose weather provider:");
            Console.WriteLine($"1 - OpenWeather");
            Console.WriteLine($"2 - WeatherApi");
            Console.WriteLine($"3 - WeatherBit");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int provider))
            {
                switch (provider)
                {
                    case 1:
                        Configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
                        Console.WriteLine("You have chosen an OpenWeather provider.");
                        return;
                    case 2:
                        Configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
                        Console.WriteLine("You have chosen a WeatherApi provider.");
                        return;
                    case 3:
                        Configuration.SetDefaultForecastApi(ForecastApi.WeatherBit);
                        Console.WriteLine("You have chosen a WeatherBit provider.");
                        return;
                }
            }

            Console.WriteLine("You entered an invalid number!");
            SetWeatherProviderByUser();
        }

        public override void SetCityNameByUser()
        {
            Console.WriteLine("Enter city names separated by commas, please");
            string input = Console.ReadLine();

            if (!_cityValidator.IsValid(input))
            {
                Console.WriteLine("An invalid city name was entered!");
                SetCityNameByUser();
            }
            else
            {
                _cityName = input;
            }
        }

        public override void SetNumberOfForecastDaysByUser()
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
                SetNumberOfForecastDaysByUser();
            }
        }

        public override async Task<WeatherCommand> BuildCommand()
        {
            var result = new WeatherCommand(_cityName, Configuration, _amountOfDays);
            //Reset();
            return result;
        }
    }
}