using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Strategies
{
    public class CurrentWeatherStrategy : IWeatherStrategy<CurrentWeatherCommand, string>
    {
        public async Task<string> Execute(CurrentWeatherCommand command)
        {
            //some additional validation
            var result = await command.GetResultAsync();
            //some additional result builder or formatter
            return result;
        }
    }
}