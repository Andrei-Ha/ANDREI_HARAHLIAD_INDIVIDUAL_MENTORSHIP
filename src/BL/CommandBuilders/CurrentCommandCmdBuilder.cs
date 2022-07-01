using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Services;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{

    public class CurrentCmdCommandBuilder : BaseCmdCommandBuilder<CurrentWeatherCommand>
    {
        public CurrentCmdCommandBuilder(IConfiguration configuration) : base(configuration)
        {

        }
        public override async Task<CurrentWeatherCommand> BuildCommand()
        {
            ChoosingWeatherProvider();
            Console.WriteLine($"Type the name of city to get the forecast, please");
            string input = Console.ReadLine();
            return new CurrentWeatherCommand
                (input, Configuration, new CityValidator(), new ResponseBuilder(new TemperatureValidator()));
        }
    }
}