using Exadel.Forecast.Domain.Models;
using System.Collections.Generic;

namespace Exadel.Forecast.Domain.Interfaces
{
    public interface IForecastModel
    {
        ForecastModel GetForecastModel();
    }
}
