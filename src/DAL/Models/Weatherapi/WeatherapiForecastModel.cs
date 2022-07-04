using Exadel.Forecast.Domain.Interfaces;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models.Weatherapi
{
    public class WeatherapiForecastModel : IForecastModel
    {
        public Location Location { get; set; }
        public Current Current { get; set; }
        public Forecast Forecast { get; set; }

        public ForecastModel UpdateForecastModel(ForecastModel forecastModel)
        {
            forecastModel.City = Location.Name;
            forecastModel.Current.Temperature = Current.TempC;
            forecastModel.Current.Date = DateTimeOffset.FromUnixTimeSeconds(Current.LastUpdatedEpoch).DateTime;
            List<DayModel> dayList = new List<DayModel>();
            foreach (var day in Forecast.Forecastday)
            {
                dayList.Add(new DayModel()
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(day.DateEpoch).DateTime,
                    AvgTemperature = day.Day.AvgTempC,
                    MaxTemperature = day.Day.MaxTempC,
                    MinTemperature = day.Day.MinTempC
                });
            }

            forecastModel.Days = dayList;

            return forecastModel;
        }
    }
}
