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
        protected IValidator<string> _cityValidator;
        protected string _cityName = string.Empty;
        protected int _amountOfDays = 0;

        public BaseCommandBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator)
        {
            Configuration = configuration;
            _cityValidator = cityValidator;
        }

        public void Reset() 
        {
            _cityName = string.Empty;
            _amountOfDays = 0;
        }

        public void SetWeatherProvider(ForecastApi weatherProvider)
        {
            Configuration.SetDefaultForecastApi(weatherProvider);
        }

        public void SetCityName(string cityName)
        {
            _cityName = cityName;
        }

        public void SetCityName(IEnumerable<string> cityNames)
        {
            string input = string.Join(",", cityNames);
            if (!_cityValidator.IsValid(input))
            {
                _cityName = "default";
            }
            else
            {
                _cityName = string.Join(",", cityNames);
            }
        }

        public abstract Task<TCommand> BuildCommand();
    }
}