using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.Domain.Interfaces;
using Exadel.Forecast.Domain.Models;
using System;

namespace Exadel.Forecast.DAL.Models.WeatherBit
{
    public class WeatherBitCurrentModel : IForecastModel
    {
        public Datum[] Data { get; set; }
        public int Count { get; set; }

        public double GetTemperature()
        {
            return Data[0].Temp;
        }

        public ForecastModel UpdateForecastModel(ForecastModel forecastModel)
        {
            forecastModel.City = Data[0].CityName;
            forecastModel.Current.Temperature = Data[0].Temp;
            forecastModel.Current.Date = DateTimeOffset.FromUnixTimeSeconds(Data[0].LocaltimeEpoch).DateTime;

            return forecastModel;
        }
    }
}
