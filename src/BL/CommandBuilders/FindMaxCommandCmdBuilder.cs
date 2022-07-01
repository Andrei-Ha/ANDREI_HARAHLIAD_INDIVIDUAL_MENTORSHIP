using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Services;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public class FindMaxCommandCmdBuilder : BaseCommandCmdBuilder<FindMaxTemperatureCommand>
    {
        public FindMaxCommandCmdBuilder(IConfiguration configuration, IValidator<string> cityValidator) : base(configuration, cityValidator)
        {
        }

        public override async Task<FindMaxTemperatureCommand> BuildCommand()
        {
            SetWeatherProvider(ForecastApi.OpenWeather);
            SetCityNameFromUser("Enter city names separated by commas to get the maximum temperature.");

            return new FindMaxTemperatureCommand(_cityName, Configuration, new ResponseBuilder(new TemperatureValidator()));
        }
    }
}
