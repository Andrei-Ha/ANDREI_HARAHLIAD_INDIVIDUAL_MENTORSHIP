using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Services
{
    public interface ICommandBuilder<TCommand> where TCommand : ICommand
    {
        Task<TCommand> BuildCommand();
    }

    public class CurrentCommandCmdBuilder : BaseCmdCommandBuilder<CurrentWeatherCommand>
    {
        public CurrentCommandCmdBuilder(IConfiguration configuration) : base (configuration)
        {
                
        }
        public async Task<CurrentWeatherCommand> BuildCommand()
        {
            ChoosingWeatherProvider();
            Console.WriteLine($"Type the name of city to get the forecast, please");
            string input = Console.ReadLine();
            return new CurrentWeatherCommand
                (input, Configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));
        }
    }

    public class BaseCmdCommandBuilder<TCommand> : ICommandBuilder<TCommand> where TCommand : ICommand
    {

        protected IConfiguration Configuration;

        public BaseCommandBuilder(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected void ChoosingWeatherProvider()
        {
            Console.WriteLine($"PLease choose weather provider:");
            Console.WriteLine($"1 - OpenWeather");
            Console.WriteLine($"2 - WeatherApi");
            var input = Console.ReadLine();

            if (int.TryParse(input, out int provider) && provider > 0 && provider < 3)
            {
                if (provider == 1)
                {
                    Configuration.SetDefaultForecastApi(ForecastApi.OpenWeather);
                    Console.WriteLine("You have chosen an OpenWeather provider.");
                    return;
                }
                if (provider == 2)
                {
                    Configuration.SetDefaultForecastApi(ForecastApi.WeatherApi);
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