using Exadel.Forecast.Api.Builders;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.Services
{
    public class SubscriptionService : IWeatherService<string, SubscriptionModel>
    {
        public readonly SubscriptionDbContext _dbContext;
        private readonly IValidator<string> _idValidator;
        private readonly IConfiguration _configuration;

        public SubscriptionService(SubscriptionDbContext dbContext, IValidator<string> idValidator, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _idValidator = idValidator;
            _configuration = configuration;
        }

        public async Task<IEnumerable<string>> Get(SubscriptionModel query)
        {
            var validPeriodList =  _configuration.GetSection("ReportsIntervals").Get<List<int>>();
            var usersRepositoryUrl = $"{_configuration.GetValue<string>("IdentityServer:Url")}/users";
            validPeriodList.Add(0);
            var commandBuilder = new SubscriptionCommandApiBuilder(
                _dbContext, usersRepositoryUrl, query, _idValidator, new ListMembership(validPeriodList));
            var subscriptionCommand = commandBuilder.BuildCommand().Result;
            var str = await subscriptionCommand.GetResultAsync();
            return new List<string>() { str };
        }
    }
}
