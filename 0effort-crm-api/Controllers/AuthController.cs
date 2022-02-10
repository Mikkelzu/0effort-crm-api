using _0effort_crm_api.Auth;
using _0effort_crm_api.Core;
using _0effort_crm_api.Models;
using _0effort_crm_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace _0effort_crm_api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService )
        {
            this._userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null) return BadRequest(new { message = "Username or password is incorrect." });

            return Ok(user);
        }
    }
}
