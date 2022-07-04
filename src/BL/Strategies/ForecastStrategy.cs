using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Strategies
{
    public class ForecastStrategy : AbstractWeatherStrategy<string>
    {
        public ForecastStrategy(
            IValidator<double> temperatureValidator,
            bool debugInfo) : base(temperatureValidator, debugInfo)
        {
        }

        public override async Task<string> Execute(ICommand weatherCommand)
        {
            var debugForecastModelList = await weatherCommand.GetResultAsync();
            StringBuilder sb = new StringBuilder();

            foreach (var dm in debugForecastModelList)
            {
                if (dm.Model != null)
                {
                    sb.AppendLine($"{dm.Model.City} maximum daily temperature forecast:");
                    int i = 1;
                    foreach (var day in dm.Model.Days)
                    {
                        sb.Append($"Day {i++} ({day.Date:dd.MM.yyyy}): ");
                        sb.AppendLine($"In {dm.Model.City} {day.MaxTemperature} °C. {GetCommentByTemp(day.MaxTemperature)}");
                    }
                }
                else
                {
                    sb.AppendLine($"Exception: {dm.TextException} Request Duration: {dm.RequestDuration}");
                }
            }

            return sb.ToString();
        }
    }
}
