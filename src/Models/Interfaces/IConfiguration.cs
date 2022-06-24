using System;
using System.Collections.Generic;
using System.Text;
using Exadel.Forecast.DAL.Interfaces;

namespace Exadel.Forecast.Models.Interfaces
{
    public interface IConfiguration
    {
        IWebApiRepository GetDefaultForecastApi();
    }
}
