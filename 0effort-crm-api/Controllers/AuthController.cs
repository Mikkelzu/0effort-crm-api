using _0effort_crm_api.Auth;
using _0effort_crm_api.Core;
using _0effort_crm_api.Models;
using _0effort_crm_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace _0effort_crm_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;
        private readonly AppSettings _appSettings;

        public AuthController(IUserService userService, IOptions<AppSettings> appSettings )
        {
            _userService = userService;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userService.Authenticate(model);

            if (user == null) return BadRequest(new { message = "Username or password is incorrect." });

            return Ok(user);
        }
    }
}
