using System;
using System.Collections.Generic;
using System.Text;
using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.Models.Interfaces
{
    public interface IConfiguration
    {
        int MaxAmountOfDays { get; set; }
        int MinAmountOfDays { get; set; }
        bool DebugInfo { get; set; }
        void SetDefaultForecastApi(ForecastApi forecastApi);
        IWebApiRepository GetDefaultForecastApi();
    }
}
