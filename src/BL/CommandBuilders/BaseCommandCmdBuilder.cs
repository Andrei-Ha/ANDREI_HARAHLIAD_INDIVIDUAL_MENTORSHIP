using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public class BaseCommandCmdBuilder<TCommand> : ICommandBuilder<TCommand> where TCommand : ICommand
    {
        protected IConfiguration Configuration;
        protected IValidator<string> _cityValidator;
        protected string _cityName;

        public BaseCommandCmdBuilder(IConfiguration configuration, IValidator<string> cityValidator)
        {
            Configuration = configuration;
            _cityValidator = cityValidator;
        }

        public void SetWeatherProvider(ForecastApi weatherProvider)
        {
            Configuration.SetDefaultForecastApi(weatherProvider);
        }

        public void SetWeatherProviderFromUser()
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
            SetWeatherProviderFromUser();
        }

        public void SetCityName(string cityName)
        {
            _cityName = cityName;
        }

        public void SetCityNameFromUser(string invite = "Type the name of city to get the forecast, please")
        {
            Console.WriteLine(invite);
            string input = Console.ReadLine();

            if (!_cityValidator.IsValid(input))
            {
                Console.WriteLine("An invalid city name was entered!");
                SetCityNameFromUser();
            }
            else
            {
                _cityName = input;
            }
        }

        public virtual Task<TCommand> BuildCommand()
        {
            throw new NotImplementedException();
        }
    }
}