using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Services
{
    public class ResponseBuilder : IResponseBuilder
    {
        private readonly IValidator<double> _temperatureValidator;

        public ResponseBuilder(IValidator<double> temperatureValidator)
        {
            _temperatureValidator = temperatureValidator;
        }

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

        public string BuildCurrent(DebugModel<CurrentModel> dm, bool debugInfo = false)
        {
            string info = debugInfo ? $" Request duration: {dm.RequestDuration}" : string.Empty;
            if (dm.Model == null)
            {
                return $"Exception: \"{dm.TextException}\"{info}";
            }
            else
            {

                if (!_temperatureValidator.IsValid(dm.Model.Temperature))
                {
                    return $"The service returned the wrong temperature!{Environment.NewLine}Exception:{dm.TextException}";
                }

                return $"In {dm.Model.City} {dm.Model.Temperature} °C.  {GetCommentByTemp(dm.Model.Temperature)}." +
                    $" Date: {dm.Model.Date:dd.MM.yyyy}  {info}";
            }
        }

        public string BuildForecast(ForecastModel fm)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{fm.City} maximum daily temperature forecast:");
            int i = 1;
            foreach (var day in fm.Days)
                {
                    sb.Append($"Day {i++} ({day.Date:dd.MM.yyyy}): ");
                    sb.AppendLine($"In {fm.City} {day.MaxTemperature} °C. {GetCommentByTemp(day.MaxTemperature)}");
                }
            return sb.ToString();
        }

        public string BuildMaxCurrent(List<DebugModel<CurrentModel>> list, bool debugInfo = false)
        {
            double maxTemp = -273;
            string cityMaxTemp = string.Empty;
            int successCount = 0, failCount = 0;
            StringBuilder debugSB = new StringBuilder($"Debug info:{Environment.NewLine}");

            foreach (var dm in list)
            {
                if (dm.Model != null && _temperatureValidator.IsValid(dm.Model.Temperature))
                {
                    if (dm.Model.Temperature > maxTemp)
                    {
                        maxTemp = dm.Model.Temperature;
                        cityMaxTemp = dm.Model.City;
                    }

                    debugSB.AppendLine($" --- City: {dm.Model.City}. Temperature: {dm.Model.Temperature}. Timer: {dm.RequestDuration} ms.");
                    successCount++;
                }
                else
                {
                    debugSB.AppendLine($" --- Exception: {dm.TextException} Timer: {dm.RequestDuration} ms.");
                    failCount++;
                }
            }

            string result;

            if (maxTemp > -273)
            {
                result = $"City with the highest temperature {maxTemp} °C: {cityMaxTemp}. Successful request count: {successCount}, failed: {failCount}.";
            }
            else
            {
                result = $"Error, no successful requests.Failed requests count: {failCount}";
            }

            //result += " Time: " + stopwatchAll.ElapsedMilliseconds + " milliseconds";

            if (debugInfo)
            {
                result += Environment.NewLine + debugSB.ToString();
            }

            return result;
        }
    }
}
