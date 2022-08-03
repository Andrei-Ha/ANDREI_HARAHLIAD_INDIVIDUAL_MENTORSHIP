using Exadel.Forecast.DAL.Interfaces;
using Exadel.Forecast.DAL.Models;
using Exadel.Forecast.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string _url;

        public UsersRepository(string url)
        {
            _url = url;
        }

        public async Task<List<UserModel>> GetUsersAsync(string id = "")
        {
            string webUrl = _url + $"?id={id}";
            var requestSender = new RequestSender<List<UserModel>>(webUrl);

            return await requestSender.GetModelAsync();
        }
    }
}
