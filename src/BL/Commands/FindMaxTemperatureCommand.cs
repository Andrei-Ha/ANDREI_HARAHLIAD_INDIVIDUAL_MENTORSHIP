using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Models.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class FindMaxTemperatureCommand : ICommand
    {
        private readonly IConfiguration _configuration;
        private readonly IValidator<string> _cityValidator;
        private readonly IValidator<double> _temperatureValidator;
        private readonly string _cityNames;

        public FindMaxTemperatureCommand
            (
                string cityNames,
                IConfiguration configuration,
                IValidator<string> cityValidator,
                IValidator<double> temperatureValidator
            )
        {
            _configuration = configuration;
            _cityValidator = cityValidator;
            _temperatureValidator = temperatureValidator;
            _cityNames = cityNames;
        }

        public async Task<string> GetResultAsync()
        {
            if (!_cityValidator.IsValid(_cityNames))
            {
                return "You need to type name of the city!";
            }

            var forecastRepository = _configuration.GetDefaultForecastApi();
            string[] cityNames = _cityNames.Split(',').Select(p => p.Trim()).ToArray();
            Task<CurrentResponseModel>[] tasks = new Task<CurrentResponseModel>[cityNames.Length];
            Stopwatch stopwatchAll = new Stopwatch();
            stopwatchAll.Start();
            for (int i = 0; i < cityNames.Length; i++)
            {
                tasks[i] = forecastRepository.GetTempByNameAsync(cityNames[i]);
            }
            CurrentResponseModel[] responses = await Task.WhenAll(tasks);
            stopwatchAll.Stop();

            double maxTemp = -273;
            string cityMaxTemp = string.Empty;
            int successCount = 0, failCount = 0;
            StringBuilder debugSB = new StringBuilder($"Debug info:{Environment.NewLine}");

            for (int i = 0; i < cityNames.Length; i++)
            {
                if (_temperatureValidator.IsValid(responses[i].Temperature))
                {
                    if (responses[i].Temperature > maxTemp)
                    {
                        maxTemp = responses[i].Temperature;
                        cityMaxTemp = responses[i].City;
                    }

                    debugSB.AppendLine($" --- City: {responses[i].City}. Temperature: {responses[i].Temperature}. Timer: {responses[i].RequestDuration} ms.");
                    successCount++;
                }
                else
                {
                    debugSB.AppendLine($" --- City: {responses[i].City}. Error: {responses[i].TextException} Timer: {responses[i].RequestDuration} ms.");
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

            result += " Time: " + stopwatchAll.ElapsedMilliseconds + " milliseconds";

            if (_configuration.DebugInfo)
            {
                result += Environment.NewLine + debugSB.ToString();
            }

            return result;
        }
    }
}
