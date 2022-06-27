using Exadel.Forecast.BL;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp
{
    internal class Program
    {
        private static IConfiguration _configuration;
        private static ICommand _command;

        static void Main(string[] args)
        {
            InitConfiguration();
            string input = string.Empty;
            while (input != "0")
            {
                StringBuilder sb = new StringBuilder($" --- Menu --- {Environment.NewLine}");
                sb.AppendLine("1 - Current weather");
                sb.AppendLine("2 - Weather forecast");
                sb.AppendLine("0 - Close application");
                Console.WriteLine(sb.ToString());
                input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        _command = CurrentWeather();
                        break;
                    case "2":
                        _command = WeatherForecast();
                        break;
                    case "0":
                        _command = new StringCommand("Bye!");
                        break;
                    default:
                        _command = new StringCommand("Wrong input!");
                        break;
                }

                Console.WriteLine(_command.GetResult());
            }
        }

        private static ICommand CurrentWeather()
        {
            ChoosingWeatherProvider();
            string invitation = $"Type the name of city to get the forecast, please";
            Console.WriteLine(invitation);
            string input = Console.ReadLine();
            return new CurrentWeatherCommand(input, _configuration, new CityValidator(), new TemperatureValidator(), new ResponseBuilder());
        }

        private static ICommand WeatherForecast() 
        {
            _configuration.SetDefaultForecastApi(ForecastApi.WeatherBit);
            string minValue = System.Configuration.ConfigurationManager.AppSettings["MinValue"];
            string maxValue = System.Configuration.ConfigurationManager.AppSettings["MaxValue"];
            int daysNumber = 0;
            string input;
            while (true)
            {
                if (daysNumber == 0)
                {
                    Console.WriteLine($"Set the number of forecast days between {minValue} and {maxValue}! \"0\" - return to previous menu");
                    input = Console.ReadLine();
                    if (int.TryParse(input, out int value))
                    {
                        var forecastNumberValidator = new ForecastNumberValidator(int.Parse(minValue), int.Parse(maxValue));
                        if (forecastNumberValidator.IsValid(value))
                        {
                            daysNumber = value;
                        }
                        else
                        {
                            Console.WriteLine("Wrong number!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Type the name of city to get the forecast, please!");
                    input = Console.ReadLine();
                    break;
                }
            }

            return new ForecastWeatherCommand(input, daysNumber, _configuration, new CityValidator(), new ResponseBuilder());
        }

        private static void InitConfiguration()
        {
            _configuration = new Configuration()
            {
                OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY"),
                WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY"),
                WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY")
            };
        }

        private static void ChoosingWeatherProvider()
        {
            Console.WriteLine($"PLease choose weather provider:");
            Console.WriteLine($"1 - OpenWeather");
            Console.WriteLine($"2 - WeatherApi");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int provider) && provider > 0 && provider < 3)
            {
                if (provider == 1)
                {
                    _configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
                    Console.WriteLine("You have chosen an OpenWeather provider.");
                    return;
                }
                if (provider == 2)
                {
                    _configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
                    Console.WriteLine("You have chosen a WeatherApi provider.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("You entered an invalid number!");
                ChoosingWeatherProvider();
            }
        }
    }
}
