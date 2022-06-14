using Exadel.Forecast.DAL;
using System;

namespace Exadel.Forecast.BL
{
    public static class Handler
    {
        public static string GetWeatherByName(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                return "You need to type name of the city!";
            }

            float temperature = OpenWeather.GetTemperature(city).Result;
            string comment;
            if (temperature == -273)
            {
                return "There is no forecast for your city!";
            }

            switch (temperature)
            {
                case float i when i < 0:
                    comment = "Dress warmly";
                    break;
                case float i when i >= 0 && i <20:
                    comment = "It's fresh";
                    break;
                case float i when i >= 20 && i <30:
                    comment = "Good weather";
                    break;
                case float i when i >= 30:
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
