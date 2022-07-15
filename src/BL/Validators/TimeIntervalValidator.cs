using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Validators
{
    public class TimeIntervalValidator
    {
        public bool IsValid(DateTime? startDateTime, DateTime? endDateTime)
        {
            if(startDateTime != null && endDateTime != null && startDateTime <= endDateTime)
            {
                return true;
            }

            return false;
        }
    }
}
