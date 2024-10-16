using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using WebApiLocationSearch.Models;
using WebApiLocationSearch.Services;

namespace WebApiLocationSearch.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromHeader(Name = "Authorization")] string authHeader)
        {
            var apiKey = _userService.Authenticate(authHeader);

            if (apiKey == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(new { apiKey });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginModel model)
        {
            if (model.Username == null || model.Password == null)
            {
                return BadRequest(new { message = "Username or password is missing." });
            }
            if (model.Username == model.Password)
            {
                return BadRequest(new { message = "Username can't be the same as password." });
            }

            if (_userService.IsUserRegistered(model.Username))
            {
                return BadRequest(new { message = "Username is already taken." });
            }
            var apiKey = await _userService.Register(model, HttpContext);

            if (apiKey == null)
                return BadRequest(new { message = "Registration failed" });

            return Ok(new { apiKey });
        }
    }
}