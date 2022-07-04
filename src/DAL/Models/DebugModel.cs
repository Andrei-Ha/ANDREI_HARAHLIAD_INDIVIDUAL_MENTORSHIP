using System;
using System.Collections.Generic;
using System.Text;

namespace Exadel.Forecast.DAL.Models
{
    public class DebugModel<T>
    { 
        public T Model { get; set; } = default;
        public long RequestDuration { get; set; } = 0;
        public string TextException { get; set; }  = string.Empty;
    }
}
