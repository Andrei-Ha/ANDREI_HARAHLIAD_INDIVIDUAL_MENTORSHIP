using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Strategies
{
    public class CurrentWeatherStrategy : AbstractWeatherStrategy<string>
    {
        public CurrentWeatherStrategy(
            IValidator<double> temperatureValidator,
            bool debugInfo) : base(temperatureValidator, debugInfo)
        {
        }

        public override async Task<string> Execute(ICommand command)
        {
            //some additional validation
            var debugForecastModelList = await command.GetResultAsync();
            //some additional result builder or formatter

            StringBuilder sb = new StringBuilder();
            foreach (var dm in debugForecastModelList)
            {
                string info = _debugInfo ? $" Request duration: {dm.RequestDuration}" : string.Empty;
                if (dm.Model == null)
                {
                    sb.AppendLine($"Exception: \"{dm.TextException}\"{info}");
                }
                else
                {

                    if (!_temperatureValidator.IsValid(dm.Model.Current.Temperature))
                    {
                        sb.AppendLine($"The service returned the wrong temperature!{Environment.NewLine}Exception:{dm.TextException}");
                    }

                    sb.AppendLine($"In {dm.Model.City} {dm.Model.Current.Temperature} °C. " +
                        $" {GetCommentByTemp(dm.Model.Current.Temperature)}. Date: {dm.Model.Current.Date:dd.MM.yyyy}  {info}");
                }
            }

            return sb.ToString();
        }
    }
}