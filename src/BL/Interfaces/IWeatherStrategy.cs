using Exadel.Forecast.BL.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IWeatherStrategy<TCommand, TResponse> 
        where TCommand : ICommand
    {
        Task<TResponse> Execute(TCommand command);
    }

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