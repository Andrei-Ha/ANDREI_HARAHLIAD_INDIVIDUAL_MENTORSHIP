using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Strategies
{
    public abstract class AbstractWeatherStrategy<TResponse> : IWeatherStrategy<TResponse>
    {
        protected readonly IValidator<double> _temperatureValidator;
        protected readonly bool _debugInfo;

        public AbstractWeatherStrategy(IValidator<double> temperatureValidator, bool debugInfo)
        {
            _temperatureValidator = temperatureValidator;
            _debugInfo = debugInfo;
        }

        protected string GetCommentByTemp(double temperature)
        {
            switch (temperature)
            {
                case double i when i < 0:
                    return "Dress warmly";
                case double i when i >= 0 && i < 20:
                    return "It's fresh";
                case double i when i >= 20 && i < 30:
                    return "Good weather";
                case double i when i >= 30:
                    return "It's time to go to the beach";
                default:
                    return "something went wrong...";
            }
        }

        public abstract Task<TResponse> Execute(ICommand weatherCommand);
    }
}
