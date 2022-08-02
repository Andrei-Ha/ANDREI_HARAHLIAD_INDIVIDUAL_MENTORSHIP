using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.Services
{
    public class SubscriptionService : IWeatherService<string, SubscriptionModel>
    {
        public readonly SubscriptionDbContext _dbContext;

        public async Task<IEnumerable<string>> Get(SubscriptionModel query)
        {
            string str = $"userId: {query.UserId}, {string.Join(",", query.Cities)}, {query.Hours}";
            var list = new List<string>() { str};
            return list;
        }
    }
}
