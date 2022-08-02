using Exadel.Forecast.BL.Interfaces;
using Exadel.Forecast.DAL.EF;
using Exadel.Forecast.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.BL.Commands
{
    public class SubscriptionCommand : ICommand<string>
    {
        private readonly SubscriptionDbContext _dbContext;
        private readonly string _userId;
        private readonly List<string> _cities;
        private readonly int _hours;

        public SubscriptionCommand(SubscriptionDbContext dbContext, string userId, List<string> cities, int hours)
        {
            _dbContext = dbContext;
            _userId = userId;
            _cities = cities;
            _hours = hours;
        }

        public async Task<string> GetResultAsync()
        {
            var response = "unsubscribed";
            var subscription = await _dbContext.SubscriptionModels.FirstOrDefaultAsync(p => p.UserId == _userId) ;

            if (subscription != null)
            {
                _dbContext.SubscriptionModels.Remove(subscription);
            }

            if (_cities.Count > 0 && _hours > 0)
            {
                await _dbContext.SubscriptionModels.AddAsync(new SubscriptionModel()
                {
                    UserId = _userId,
                    Cities = _cities,
                    Hours = _hours
                });
                response = "subscribed";
            }

            await _dbContext.SaveChangesAsync();

            return response;
        }
    }
}
