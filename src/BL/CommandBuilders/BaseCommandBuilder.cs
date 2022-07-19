using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public abstract class BaseCommandBuilder<TCommand> : ICommandBuilder<TCommand>
        where TCommand : ICommand
    {
        protected IConfiguration Configuration;
        protected IValidator<string> CityValidator;
        protected string CityName = string.Empty;
        protected int AmountOfDays = 0;

        public BaseCommandBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator)
        {
            Configuration = configuration;
            CityValidator = cityValidator;
        }

        public void Reset() 
        {
            CityName = string.Empty;
            AmountOfDays = 0;
        }

        public void SetWeatherProvider(ForecastApi weatherProvider)
        {
            Configuration.SetDefaultForecastApi(weatherProvider);
        }

        public void SetCityName(string cityName)
        {
            CityName = cityName;
        }

        public void SetCityName(IEnumerable<string> cityNames)
        {
            string input = string.Join(",", cityNames);
            if (!CityValidator.IsValid(input))
            {
                CityName = "default";
            }
            else
            {
                CityName = string.Join(",", cityNames);
            }
        }

        public abstract Task<TCommand> BuildCommand();
    }
}