using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IWebApiRepository
    {
        double GetTempByName(string cityName);
        double[] GetForecastByName(string cityName);
    }
}
