using Exadel.Forecast.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Validators
{
    public class CityValidator: IValidator<string>
    {
        public bool IsValid(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}
