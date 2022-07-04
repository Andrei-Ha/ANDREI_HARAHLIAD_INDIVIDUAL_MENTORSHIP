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

    public class CurrentCommandCmdBuilder : BaseCommandCmdBuilder
    {
        public CurrentCommandCmdBuilder(
            IConfiguration configuration,
            IValidator<string> validator) : base(configuration, validator)
        {
        }

        public override Task<WeatherCommand> BuildCommand()
        {
            SetWeatherProviderByUser();
            SetCityNameByUser();
            return Task.FromResult(new WeatherCommand(_cityName, Configuration, _amountOfDays));
        }
    }
}