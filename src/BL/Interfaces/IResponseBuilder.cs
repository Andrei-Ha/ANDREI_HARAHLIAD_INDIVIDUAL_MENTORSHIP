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
        string BuildForecast(ForecastModel model);
        string BuildMaxCurrent(List<DebugModel<CurrentModel>> model, bool debugInfo = false);
    }


    public interface IResponseBuilder<TModel, TResponse>
        where TModel : class
    {
        TResponse BuildResponse(TModel model, bool debugInfo = false);
    }
}
