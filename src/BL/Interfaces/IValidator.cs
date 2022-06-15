using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IValidator<TValue>
    {
        bool IsValid(TValue value);
    }
}
