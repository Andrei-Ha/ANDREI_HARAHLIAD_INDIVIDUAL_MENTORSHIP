using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IDbRepository
    {
        Task<List<ForecastModel>> GetHistoryAsync(List<string> cities, DateTime startDateTime, DateTime endDateTime);
    }
}
