using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Text;

namespace Exadel.Forecast.BL
{
    public class Handler
    {
        private readonly IConfiguration _configuration;
        private readonly IValidator<string> _validator;
        private readonly IResponseBuilder _responseBuilder;

        public Handler(IConfiguration configuration, IValidator<string> validator, IResponseBuilder responseBuilder)
        {
            _configuration = configuration;
            _validator = validator;
            _responseBuilder = responseBuilder;
        }

        public string GetWeatherByName(string city)
        {
            if (!_validator.IsValid(city))
            {
                return "You need to type name of the city!";
            }

            double temperature = _configuration.GetDefaultForecastApi().GetTempByName(city);
            return _responseBuilder.WeatherStringByTemp(city, temperature);
        }

        public string GetForecastByName(string city)
        {
            if (!_validator.IsValid(city))
            {
                return "You need to type name of the city!";
            }

            double[] temps = _configuration.GetDefaultForecastApi().GetForecastByName(city);
            var sb = new StringBuilder();
            sb.AppendLine($"{city} weather forecast:");
            int i = 1;
            foreach (var temp in temps)
            {
                sb.Append($"Day {i++}: ");
                sb.AppendLine(_responseBuilder.WeatherStringByTemp(city, temp));
            }
            return sb.ToString();
        }
    }
}
