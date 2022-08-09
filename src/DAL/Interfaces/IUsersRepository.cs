using Exadel.Forecast.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exadel.Forecast.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<IdentityUser>> GetUsersAsync(string id);
    }
}
