using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // POST: api/auth/register
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var (success, message) = await _authService.RegisterUserAsync(model);
        if (!success)
            return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = message });

        return Ok(new { Status = "Success", Message = message });
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var (authenticated, token, errorMessage) = await _authService.ValidateUserAsync(model);
        if (authenticated)
        {
            return Ok(new
            {
                token = token,
                expiration = DateTime.Now.AddHours(3)  // Adjust according to your token lifetime settings
            });
        }
        return Unauthorized(new { Status = "Error", Message = errorMessage });
    }
}
