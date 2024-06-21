using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Services;
using System.Net;
using System.Security.Claims;

namespace MongoAuthenticatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.RegisterAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponse))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authenticationService.LoginAsync(request);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet]
        [Authorize]
        [Route("me/{userId}")]
        public async Task<IActionResult> GetMyProfile(string userId)
        {
            var result = await _authenticationService.GetMyProfileAsync(userId);
            return result.Success ? Ok(result) : NotFound(result.Message);
        }

        [HttpPatch]
        [Authorize]
        [Route("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authenticationService.UpdateProfileAsync(request, userEmail);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpPatch]
        [Authorize]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userEmail = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
   
            var result = await _authenticationService.ChangePasswordAsync(request, userEmail);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }

        [HttpGet]
        [Authorize]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _authenticationService.GetUsersAsync();
            return Ok(users);
        }
    }
}
