using Exadel.Forecast.IdentityServer.Data;
using Exadel.Forecast.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exadel.Forecast.IdentityServer.Controllers
{
    [ApiController]
    [Route("Users")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController :ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet("test")]
        public async Task<IActionResult> GetTest()
        {
            return Ok("test");
        }
        

        [HttpGet]
        [Authorize(Policy ="ForecastApiClient")]
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
