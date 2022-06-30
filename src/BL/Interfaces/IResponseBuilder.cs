using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IResponseBuilder
    {
        string BuildCurrent(DebugModel<CurrentModel> model, bool debugInfo = false);
        string BuildForecast(ForecastModel model, int amountOfDays);
        string BuildMaxCurrent(List<DebugModel<CurrentModel>> model, bool debugInfo = false);
    }
}
