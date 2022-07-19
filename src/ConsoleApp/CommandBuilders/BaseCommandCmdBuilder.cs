using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp.CommandBuilders
{
    public abstract class BaseCommandCmdBuilder : BaseCommandBuilder<WeatherCommand>
    {
        const string wrongCityName = "An invalid city name was entered!";

        public BaseCommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator) : base(configuration, cityValidator)
        {
        }

        public void SetWeatherProviderByUser()
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

        public override void SetCityName(string cityName)
        {
            if (!CityValidator.IsValid(CityName = cityName))
            {
                Console.WriteLine(wrongCityName);
            };
        }

        public override void SetCityName(IEnumerable<string> cityNames)
        {
            string input = string.Join(",", cityNames);
            if (!CityValidator.IsValid(input))
            {
                Console.WriteLine(wrongCityName);
            }
            else
            {
                CityName = string.Join(",", cityNames);
            }
        }

        public void SetCityNameByUser()
        {
            Console.WriteLine("Enter city names separated by commas, please");
            string input = Console.ReadLine();

            if (!CityValidator.IsValid(input))
            {
                Console.WriteLine(wrongCityName);
                SetCityNameByUser();
            }
            else
            {
                CityName = input;
            }
        }

        public override abstract Task<WeatherCommand> BuildCommand();
    }
}