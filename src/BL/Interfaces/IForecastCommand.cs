using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Interfaces
{
    public interface IForecastCommand : ICommand<List<DebugModel<ForecastModel>>>
    {
    }
}
