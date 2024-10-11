[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
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
    public IActionResult Register([FromBody] LoginModel model)
    {
        var apiKey = _userService.Register(model);

        if (apiKey == null)
            return BadRequest(new { message = "Registration failed" });

        return Ok(new { apiKey }); // Return the API key
    }
}