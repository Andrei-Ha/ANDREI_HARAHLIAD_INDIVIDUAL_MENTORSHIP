using Exadel.Forecast.BL;
using Exadel.Forecast.BL.CommandBuilders;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Services;
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
        private static ICommand _command;

        static async Task Main(string[] args)
        {
            InitConfiguration();
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
                        //IWeatherStrategy<CurrentWeatherCommand,string> currentStrategy = new CurrentWeatherStrategy();
                        ICommandBuilder<CurrentWeatherCommand> currentCommandBuilder = new CurrentCommandCmdBuilder(_configuration, new CityValidator());
                        CurrentWeatherCommand currentCommand = await currentCommandBuilder.BuildCommand();

                        //string result = await currentStrategy.Execute(currentCommand);

                        _command = currentCommand;
                        break;
                    case "2":
                        var forecastNumberValidator = 
                            new ForecastNumberValidator(_configuration.MinAmountOfDays, _configuration.MaxAmountOfDays);

                        ICommandBuilder<ForecastWeatherCommand> forecastCommandBuilder =
                            new ForecastCommandCmdBuilder(_configuration, new CityValidator(), forecastNumberValidator);

                        ForecastWeatherCommand forecastCommand = await forecastCommandBuilder.BuildCommand();
                        _command = forecastCommand;
                        break;
                    case "3":
                        ICommandBuilder<FindMaxTemperatureCommand> findMaxTemperatureCommandBuilder =
                            new FindMaxCommandCmdBuilder(_configuration, new CityValidator());

                        FindMaxTemperatureCommand findMaxTemperatureCommand = await findMaxTemperatureCommandBuilder.BuildCommand();
                        _command = findMaxTemperatureCommand;
                        break;
                    case "0":
                        _command = new StringCommand("Bye!");
                        break;
                    default:
                        _command = new StringCommand("Wrong input!");
                        break;
                }

                Console.WriteLine(await _command.GetResultAsync());
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
                DebugInfo = bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["DebugInfo"], out bool value) && value
            };
        }
    }
}
