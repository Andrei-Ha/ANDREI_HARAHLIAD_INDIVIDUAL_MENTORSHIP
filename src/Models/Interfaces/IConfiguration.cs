﻿using System;
using System.Collections.Generic;
using System.Text;
using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Models.Configuration;

namespace Exadel.Forecast.Models.Interfaces
{
    public interface IConfiguration
    {
        int MaxAmountOfDays { get; set; }
        int MinAmountOfDays { get; set; }
        bool DebugInfo { get; set; }
        int ExecutionTime { get; set; }
        List<int> ReportsIntervals { get; set; }
        string UsersEndpointUrl { get; set; }
        ClientCredentialsRequest ClientCredential { get; set; }
        void SetDefaultForecastApi(ForecastApi forecastApi);
        IWebApiRepository GetDefaultForecastApi();
    }
}
