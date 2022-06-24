using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IResponseBuilder
    {
        string WeatherStringByTemp(string city, double temperature);
    }
}
