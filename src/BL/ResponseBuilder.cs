using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL
{   
    public class ResponseBuilder : IResponseBuilder
    {
        private string GetCommentByTemp(double temperature)
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

        public string WeatherStringByTemp(CurrentResponseModel model, bool debugInfo = false)
        {
            string info = debugInfo ? $" Time: {model.RequestDuration}" : string.Empty;
            return $"In {model.City} {model.Temperature} °C. {GetCommentByTemp(model.Temperature)}{info}";
        }

        public string WeatherStringByTemp(ForecastResponseModel model)
        {
            return $"In {model.City} {model.Temperature} °C. {GetCommentByTemp(model.Temperature)}";
        }
    }
}
