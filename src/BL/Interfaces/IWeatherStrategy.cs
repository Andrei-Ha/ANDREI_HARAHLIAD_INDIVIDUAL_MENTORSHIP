using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    //public interface IWeatherStrategy<TCommand, TResponse> 
    //    where TCommand : IForecastCommand
    //{
    //    Task<TResponse> Execute(TCommand command);
    //}

    public interface IWeatherStrategy<TResponse>
    {
        Task<TResponse> Execute(IForecastCommand weatherCommand);
    }
}