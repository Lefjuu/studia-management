// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;

// [ApiController]
// [Route("users")]
// public class UsersController : ControllerBase
// {
//   private readonly UserService _userService;

//   public UsersController(UserService userService)
//   {
//     _userService = userService;
//   }

//   // GET: users/{id}
//   [HttpGet("{id}")]
//   public async Task<IActionResult> GetUser(Guid id)
//   {
//     var user = await _userService.GetUserAsync(id);
//     if (user == null)
//     {
//       return NotFound();
//     }
//     return Ok(user);
//   }

//   // GET: users
//   [HttpGet]
//   public async Task<IActionResult> GetAllUsers()
//   {
//     var users = await _userService.GetAllUsersAsync();
//     return Ok(users);
//   }

//   // POST: users
//   [HttpPost]
//   public async Task<IActionResult> CreateUser([FromBody] User newUser)
//   {
//     await _userService.CreateUserAsync(newUser);
//     return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
//   }

//   // PATCH: users/{id}
//   [HttpPatch("{id}")]
//   public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
//   {
//     var user = await _userService.GetUserAsync(id);
//     if (user == null)
//     {
//       return NotFound();
//     }

//     await _userService.UpdateUserAsync(id, updatedUser);
//     return NoContent();
//   }

//   // DELETE: users/{id}
//   [Authorize]
//   [HttpDelete("{id}")]
//   public async Task<IActionResult> DeleteUser(Guid id)
//   {
//     var user = await _userService.GetUserAsync(id);
//     if (user == null)
//     {
//       return NotFound();
//     }

//     await _userService.DeleteUserAsync(id);
//     return NoContent();
//   }
// }
