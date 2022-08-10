using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using ModelsConfiguration = Exadel.Forecast.Models.Interfaces;

namespace Exadel.Forecast.Api.Builders
{
    public class SubscriptionCommandApiBuilder : ISubscriptionCommandBuilder
    {
        private readonly WeatherDbContext _dbContext;
        private readonly ModelsConfiguration.IConfiguration _configuration;
        private readonly SubscriptionModel _subscriptionModel;
        private readonly IValidator<string> _idValidator;
        private readonly IValidator<int> _listMembershipValidator;
        private string _userId = string.Empty;
        private List<string> _cities = new();
        private int _hours;

        public SubscriptionCommandApiBuilder(
            WeatherDbContext dbContext,
            ModelsConfiguration.IConfiguration configuration,
            SubscriptionModel subscriptionModel,
            IValidator<string> idValidator,
            IValidator<int> listMembershipValidator)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _subscriptionModel = subscriptionModel;
            _idValidator = idValidator;
            _listMembershipValidator = listMembershipValidator;
        }

        private void SetUserId()
        {
            if (!_idValidator.IsValid(_subscriptionModel.UserId))
            {
                throw new HttpRequestException("UserId is required field!", null, System.Net.HttpStatusCode.UnprocessableEntity);
            };
            _userId = _subscriptionModel.UserId;
        }

        private void SetCities() 
        {
            _cities = _subscriptionModel.Cities;
        }

        private void SetHours()
        {
            if (!_listMembershipValidator.IsValid(_subscriptionModel.Hours))
            {
                throw new HttpRequestException("Wrong value of Hours!", null, System.Net.HttpStatusCode.UnprocessableEntity);
            }

            _hours = _subscriptionModel.Hours;
        }

        public Task<SubscriptionCommand> BuildCommand()
        {
            SetUserId();
            SetCities();
            SetHours();
            return Task.FromResult(new SubscriptionCommand(_dbContext, _configuration, _userId, _cities, _hours));
        }
    }
}
