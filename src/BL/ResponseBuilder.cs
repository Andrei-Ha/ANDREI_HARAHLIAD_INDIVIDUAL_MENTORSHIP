using Exadel.Forecast.BL.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL
{
    public class ResponseBuilder
    {
        private readonly TemperatureValidator _temperatureValidator;
        public ResponseBuilder(TemperatureValidator temperatureValidator)
        {
            _temperatureValidator = temperatureValidator;
        }

        public string WeatherStringByTemp(string city, double temperature)
        {
            string comment;
            if (!_temperatureValidator.IsValid(temperature))
            {
                return "There is no forecast for your city!";
            }

            switch (temperature)
            {
                case double i when i < 0:
                    comment = "Dress warmly";
                    break;
                case double i when i >= 0 && i < 20:
                    comment = "It's fresh";
                    break;
                case double i when i >= 20 && i < 30:
                    comment = "Good weather";
                    break;
                case double i when i >= 30:
                    comment = "It's time to go to the beach";
                    break;
                default:
                    comment = "something went wrong...";
                    break;
            }

            return $"In {city} {temperature} °C. {comment}";
        }
    }
}
