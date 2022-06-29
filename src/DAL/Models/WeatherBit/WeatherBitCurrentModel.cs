using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.Domain;
using System;

namespace Exadel.Forecast.DAL.Models.WeatherBit
{
    public class WeatherBitCurrentModel : ICurrentWeatherModel
    {
        public Datum[] Data { get; set; }
        public int Count { get; set; }

        public double GetTemperature()
        {
            return Data[0].Temp;
        }

        public CurrentModel GetCurrentModel()
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(Data[0].LocaltimeEpoch);
            return new CurrentModel() { City = Data[0].CityName, Temperature = Data[0].Temp, Date = dateTime };
        }
    }
}
