using Exadel.Forecast.IdentityServer.Data;
using Exadel.Forecast.IdentityServer.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exadel.Forecast.IdentityServer.Controllers
{
    [Route("Users")]
    public class UserController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public UserController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var users = _dbContext.Users.AsNoTracking();

            if (!string.IsNullOrEmpty(id))
            {
                users = users.Where(x => x.Id == id);
            }

            return new JsonResult(await users.Select(x => new UserDTO { Id = x.Id, UserName = x.UserName, Email = x.Email }).ToListAsync());
        }
    }
}
