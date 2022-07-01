using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Services
{
    public class HtmlResponseBuilder : IResponseBuilder
    {
        public string BuildCurrent(DebugModel<CurrentModel> model, bool debugInfo = false)
        {
            throw new NotImplementedException();
        }

        public string BuildForecast(ForecastModel model)
        {
            throw new NotImplementedException();
        }

        public string BuildMaxCurrent(List<DebugModel<CurrentModel>> model, bool debugInfo = false)
        {
            throw new NotImplementedException();
        }
    }
}
