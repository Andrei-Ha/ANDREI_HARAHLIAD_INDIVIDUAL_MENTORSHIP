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

        public abstract void SetCityName(string cityName);

        public abstract void SetCityName(IEnumerable<string> cityNames);
        
        public abstract Task<TCommand> BuildCommand();
    }
}