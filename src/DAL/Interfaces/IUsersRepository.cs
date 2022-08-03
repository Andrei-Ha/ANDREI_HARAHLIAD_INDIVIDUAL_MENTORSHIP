using Exadel.Forecast.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<UserModel>> GetUsersAsync(string id);
    }
}
