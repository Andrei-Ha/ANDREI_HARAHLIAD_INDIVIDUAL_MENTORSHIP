using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Strategies
{
    public class FindMaxTemperatureStrategy : AbstractWeatherStrategy<string>
    {
        public FindMaxTemperatureStrategy(
            IValidator<double> temperatureValidator,
            bool debugInfo) : base(temperatureValidator, debugInfo)
        {
        }

        public override async Task<string> Execute(ICommand weatherCommand)
        {
            var debugForecastModelList = await weatherCommand.GetResultAsync();
            //StringBuilder sb = new StringBuilder();

            double temperature, maxTemp = -273;
            string cityMaxTemp = string.Empty;
            int successCount = 0, failCount = 0;
            StringBuilder debugSB = new StringBuilder($"Debug info:{Environment.NewLine}");

            foreach (var dm in debugForecastModelList)
            {
                if (dm.Model != null && _temperatureValidator.IsValid(dm.Model.Current.Temperature))
                {
                    temperature = dm.Model.Current.Temperature;
                    if (temperature > maxTemp)
                    {
                        maxTemp = temperature;
                        cityMaxTemp = dm.Model.City;
                    }

                    debugSB.AppendLine($" --- City: {dm.Model.City}. Temperature: {temperature}. Timer: {dm.RequestDuration} ms.");
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

            if (_debugInfo)
            {
                result += Environment.NewLine + debugSB.ToString();
            }

            return result;
        }
    }
}
