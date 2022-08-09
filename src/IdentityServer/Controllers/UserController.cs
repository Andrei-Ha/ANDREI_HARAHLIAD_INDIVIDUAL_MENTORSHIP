using Exadel.Forecast.IdentityServer.Data;
using Exadel.Forecast.IdentityServer.DTO;
using Exadel.Forecast.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exadel.Forecast.IdentityServer.Controllers
{
    [Route("Users")]
    public class UserController :ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get([FromQuery] string id)
        {
            var users = new List<ApplicationUser>();

            if (!string.IsNullOrEmpty(id))
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    users.Add(user);
                }
            }
            else
            {
                users.AddRange(await _userManager.Users.ToListAsync());
            }

            return new JsonResult(users);
        }
    }
}
