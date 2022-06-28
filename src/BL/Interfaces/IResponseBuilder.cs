using Exadel.Forecast.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IResponseBuilder
    {
        string WeatherStringByTemp(CurrentResponseModel model, bool debugInfo = false);
        string WeatherStringByTemp(ForecastResponseModel model);
    }
}
