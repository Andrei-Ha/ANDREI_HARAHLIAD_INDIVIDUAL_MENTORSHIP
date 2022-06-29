using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain;
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

        public string WeatherStringByTemp(DebugModel<CurrentModel> dm, bool debugInfo = false)
        {
            string info = debugInfo ? $" Request duration: {dm.RequestDuration}" : string.Empty;
            if (dm.Model == null)
            {
                return $"Exception: \"{dm.TextException}\"{info}";
            }
            else
            {
                return $"In {dm.Model.City} {dm.Model.Temperature} °C.  {GetCommentByTemp(dm.Model.Temperature)}." +
                    $" Date: {dm.Model.Date:dd.MM.yyyy}  {info}";
            }
        }

        public string WeatherStringByTemp(ForecastModel fm, int forecastDays = int.MaxValue)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{fm.City} maximum daily temperature forecast:");
            int i = 1;
            foreach (var day in fm.Days) if (i <= forecastDays)
                {
                    sb.Append($"Day {i++} ({day.Date:dd.MM.yyyy}): ");
                    sb.AppendLine($"In {fm.City} {day.MaxTemperature} °C. {GetCommentByTemp(day.MaxTemperature)}");
                }
            return sb.ToString();
        }
    }
}
