using Exadel.Forecast.BL;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp
{
    internal class Program
    {
        private static Configuration _configuration;

        static void Main(string[] args)
        {
            InitConfiguration();
            ChoosingWeatherProvider();
            Handler handler = new Handler(_configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));
            string strFromUser = string.Empty;
            while (strFromUser != "q")
            {
                Console.WriteLine($"Type the name of city to get the forecast, please{Environment.NewLine} To quit type \"q\", or \"c\" to change weather provider ");
                strFromUser = Console.ReadLine();
                if (strFromUser == "c")
                {
                    ChoosingWeatherProvider();
                }
                else if (strFromUser == "q")
                {
                    break;
                }
                else
                {
                    Console.WriteLine(handler.GetWeatherByName(strFromUser));
                }
            }
        }

        private static void InitConfiguration()
        {
            _configuration = new Configuration()
            {
                OpenWeatherKey = System.Configuration.ConfigurationManager.AppSettings["OPENWEATHER_API_KEY"],
                WeatherApiKey = System.Configuration.ConfigurationManager.AppSettings["WEATHERAPI_API_KEY"]
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
                if(provider == 2)
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
