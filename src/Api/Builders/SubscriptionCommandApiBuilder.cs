using Exadel.Forecast.BL.Commands;
using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.BL.Validators;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;

namespace Exadel.Forecast.Api.Builders
{
    public class SubscriptionCommandApiBuilder : ISubscriptionCommandBuilder
    {
        private readonly SubscriptionDbContext _dbContext;
        private readonly string _usersRepositoryUrl;
        private readonly SubscriptionModel _subscriptionModel;
        private readonly IValidator<string> _idValidator;
        private readonly IValidator<int> _listMembership;
        private string _userId = string.Empty;
        private List<string> _cities = new();
        private int _hours;

        public SubscriptionCommandApiBuilder(
            SubscriptionDbContext dbContext,
            string usersRepositoryUrl,
            SubscriptionModel subscriptionModel,
            IValidator<string> idValidator,
            IValidator<int> listMembership)
        {
            _dbContext = dbContext;
            _usersRepositoryUrl = usersRepositoryUrl;
            _subscriptionModel = subscriptionModel;
            _idValidator = idValidator;
            _listMembership = listMembership;
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
            if (!_listMembership.IsValid(_subscriptionModel.Hours))
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
            return Task.FromResult(new SubscriptionCommand(_dbContext, _usersRepositoryUrl, _userId, _cities, _hours));
        }
    }
}
