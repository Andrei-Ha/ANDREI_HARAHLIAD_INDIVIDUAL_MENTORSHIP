using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.Models.Configuration;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.CommandBuilders
{
    public abstract class BaseCommandBuilder : ICommandBuilder
    {
        protected IConfiguration Configuration;
        protected IValidator<string> _cityValidator;
        protected string _cityName;
        protected readonly IValidator<int> _forecastNumberValidator;
        protected int _amountOfDays = 0;

        public BaseCommandBuilder(
            IConfiguration configuration,
            IValidator<string> cityValidator,
            IValidator<int> forecastNumberValidator)
        {
            Configuration = configuration;
            _cityValidator = cityValidator;
            _forecastNumberValidator = forecastNumberValidator;
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

        public abstract void SetWeatherProviderByUser();

        public void SetCityName(string cityName)
        {
            _cityName = cityName;
        }

        public abstract void SetCityNameByUser();

        public void SetNumberOfForecastDays(int amountOfDays)
        {
            _amountOfDays = amountOfDays;
        }

        public abstract void SetNumberOfForecastDaysByUser();

        public abstract Task<WeatherCommand> BuildCommand();
    }
}