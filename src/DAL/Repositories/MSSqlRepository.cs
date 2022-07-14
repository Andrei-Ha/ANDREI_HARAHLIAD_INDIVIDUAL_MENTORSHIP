using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class MSSqlRepository : IDbRepository
    {
        public MSSqlRepository()
        {

        }

        public Task<List<ForecastModel>> GetHistoryAsync(List<string> cities, DateTime startDateTime, DateTime endDateTime)
        {
            throw new NotImplementedException();
        }
    }
}
