using Exadel.Forecast.Api.Builders;
using Exadel.Forecast.Api.Interfaces;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Services
{
    public class SubscriptionService : IWeatherService<string, SubscriptionModel>
    {
        public readonly SubscriptionDbContext _dbContext;
        private readonly IValidator<string> _idValidator;
        private readonly ModelsConfiguration.IConfiguration _configuration;

        public SubscriptionService(
            SubscriptionDbContext dbContext,
            IValidator<string> idValidator,
            ModelsConfiguration.IConfiguration configuration)
        {
            _dbContext = dbContext;
            _idValidator = idValidator;
            _configuration = configuration;
        }

        public async Task<IEnumerable<string>> Get(SubscriptionModel query)
        {
            var validPeriodList =  _configuration.ReportsIntervals;
            validPeriodList.Add(0);
            var commandBuilder = new SubscriptionCommandApiBuilder(
                _dbContext, _configuration, query, _idValidator, new ListMembershipValidator(validPeriodList));
            var subscriptionCommand = commandBuilder.BuildCommand().Result;
            var str = await subscriptionCommand.GetResultAsync();
            return new List<string>() { str };
        }
    }
}
