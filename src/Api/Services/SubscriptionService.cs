using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.Services
{
    public class SubscriptionService : IWeatherService<string, SubscriptionModel>
    {
        public readonly SubscriptionDbContext _dbContext;

        public SubscriptionService(SubscriptionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<string>> Get(SubscriptionModel query)
        {
            var subscriptionCommand = new SubscriptionCommand(_dbContext, query.UserId,query.Cities,query.Hours);
            var str = await subscriptionCommand.GetResultAsync();
            return new List<string>() { str };
        }
    }
}
