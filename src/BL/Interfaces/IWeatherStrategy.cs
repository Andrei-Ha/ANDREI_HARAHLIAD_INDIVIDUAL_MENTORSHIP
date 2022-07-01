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
}