using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Services
{
    public class DateConverter
    {
        public DateTime UnixTimestampToDateTime(int value)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(value);
        }
    }
}
