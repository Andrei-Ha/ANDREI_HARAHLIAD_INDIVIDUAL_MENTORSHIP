using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface ICurrentWeatherModel
    {
        CurrentModel GetCurrentModel();
    }
}
