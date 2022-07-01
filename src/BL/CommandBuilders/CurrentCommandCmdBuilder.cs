using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
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

    public class CurrentCommandCmdBuilder : BaseCommandCmdBuilder<CurrentWeatherCommand>
    {
        public CurrentCommandCmdBuilder(IConfiguration configuration, IValidator<string> validator) : base(configuration, validator)
        {

        }

        public override async Task<CurrentWeatherCommand> BuildCommand()
        {
            SetWeatherProviderFromUser();
            SetCityNameFromUser();
            
            return new CurrentWeatherCommand(_cityName, Configuration, new ResponseBuilder(new TemperatureValidator()));
        }
    }
}