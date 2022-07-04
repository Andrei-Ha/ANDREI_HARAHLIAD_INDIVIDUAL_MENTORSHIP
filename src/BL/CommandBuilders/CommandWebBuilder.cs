using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public class CommandWebBuilder : BaseCommandBuilder
    {
        public CommandWebBuilder(
            IConfiguration configuration,
            IValidator<string> validator,
            IValidator<int> forecastNumberValidator) : base(configuration, validator, forecastNumberValidator)
        {
        }

        public override void SetWeatherProviderByUser()
        {
            throw new NotImplementedException();
        }

        public override void SetCityNameByUser()
        {
            throw new NotImplementedException();
        }

        public override void SetNumberOfForecastDaysByUser()
        {
            throw new NotImplementedException();
        }

        public override Task<WeatherCommand> BuildCommand()
        {
            throw new NotImplementedException();
        }
    }
}
