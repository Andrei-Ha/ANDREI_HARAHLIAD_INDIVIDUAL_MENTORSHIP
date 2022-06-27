using Exadel.Forecast.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        double GetTempByName(string cityName);
        ForecastResponseModel[] GetForecastByName(string cityName);
    }
}
