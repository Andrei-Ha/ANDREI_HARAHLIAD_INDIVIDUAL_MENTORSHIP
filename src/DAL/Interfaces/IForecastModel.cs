using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Interfaces
{
    internal interface IForecastModel
    {
        IDayForecastModel[] GetDaysForecastModel();
    }
}
