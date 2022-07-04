using Exadel.Forecast.BL;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Strategies;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.ConsoleApp
{
    internal class Program
    {
        private static IConfiguration _configuration;

        static async Task Main(string[] args)
        {
            InitConfiguration();
            ICommandBuilder<WeatherCommand> commandBuilder;
            ICommand command;
            IWeatherStrategy<string> strategy;
            string output = string.Empty;
            string input = string.Empty;
            while (input != "0")
            {
                StringBuilder sb = new StringBuilder($" --- Menu --- {Environment.NewLine}");
                sb.AppendLine("1 - Current weather");
                sb.AppendLine("2 - Weather forecast");
                sb.AppendLine("3 - Find Max current temperature in cities");
                sb.AppendLine("0 - Close application");
                Console.WriteLine(sb.ToString());
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        commandBuilder = new CurrentCommandCmdBuilder(_configuration, new CityValidator());
                        strategy = new CurrentWeatherStrategy(new TemperatureValidator(), _configuration.DebugInfo);
                        command = await commandBuilder.BuildCommand();
                        output = await strategy.Execute(command);
                        break;
                    case "2":
                        var forecastNumberValidator = new ForecastNumberValidator(_configuration.MinAmountOfDays, _configuration.MaxAmountOfDays);
                        commandBuilder = new ForecastCommandCmdBuilder(_configuration, new CityValidator(), forecastNumberValidator);
                        strategy = new ForecastStrategy(new TemperatureValidator(), _configuration.DebugInfo);
                        command = await commandBuilder.BuildCommand();
                        output = await strategy.Execute(command);
                        break;
                    case "3":
                        commandBuilder = new CurrentCommandCmdBuilder(_configuration, new CityValidator());
                        strategy = new FindMaxTemperatureStrategy(new TemperatureValidator(), _configuration.DebugInfo);
                        command = await commandBuilder.BuildCommand();
                        output = await strategy.Execute(command);
                        break;
                    default:
                        break;
                }
                
                Console.WriteLine(output);
                output = string.Empty;
            }
        }

        private static void InitConfiguration()
        {
            _configuration = new Configuration()
            {
                MinAmountOfDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MinValue"]),
                MaxAmountOfDays = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxValue"]),
                OpenWeatherKey = Environment.GetEnvironmentVariable("OPENWEATHER_API_KEY"),
                WeatherApiKey = Environment.GetEnvironmentVariable("WEATHERAPI_API_KEY"),
                WeatherBitKey = Environment.GetEnvironmentVariable("WEATHERBIT_API_KEY"),
                DebugInfo = bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["DebugInfo"], out bool value) && value,
                ExecutionTime = int.Parse(System.Configuration.ConfigurationManager.AppSettings["ExecutionTime"])
            };
        }
    }
}
