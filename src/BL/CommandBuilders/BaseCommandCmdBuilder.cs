using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public abstract class BaseCommandCmdBuilder : ICommandBuilder<WeatherCommand>
    {
        protected IConfiguration Configuration;
        protected IValidator<string> _cityValidator;
        protected string _cityName;
        protected int _amountOfDays = 0;

        public BaseCommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator)
        {
            Configuration = configuration;
            _cityValidator = cityValidator;
        }

        public void Reset() 
        {
            _cityName = string.Empty;
            _amountOfDays = 0;
        }

        public void SetWeatherProvider(ForecastApi weatherProvider)
        {
            Configuration.SetDefaultForecastApi(weatherProvider);
        }

        public void SetCityName(string cityName)
        {
            _cityName = cityName;
        }

        public void SetNumberOfForecastDays(int amountOfDays)
        {
            _amountOfDays = amountOfDays;
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

        public void SetCityNameByUser()
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

        public abstract Task<WeatherCommand> BuildCommand();
    }
}