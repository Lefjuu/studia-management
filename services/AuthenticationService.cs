using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoAuthenticatorAPI.Dtos;
using MongoAuthenticatorAPI.Models;
using MongoDbGenericRepository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MongoAuthenticatorAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
{
    try
    {
        var userExists = await _userManager.FindByEmailAsync(request.Email);
        if (userExists != null) return new RegisterResponse { Message = "User already exists", Success = false };

        var newUser = new ApplicationUser
        {
            UserName = request.FullName,
            Email = request.Email,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            FullName = request.FullName,
        };

        var createUserResult = await _userManager.CreateAsync(newUser, request.Password);
        if (!createUserResult.Succeeded) 
            return new RegisterResponse { Message = $"Create user failed: {createUserResult?.Errors?.FirstOrDefault()?.Description}", Success = false };


        var roleExists = await _roleManager.RoleExistsAsync("user");
        if (!roleExists)
        {
            var createRoleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = "user" });
            if (!createRoleResult.Succeeded) 
                return new RegisterResponse { Message = $"Create role failed: {createRoleResult?.Errors?.FirstOrDefault()?.Description}", Success = false };
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, "user");
        if (!addToRoleResult.Succeeded) 
            return new RegisterResponse { Message = $"Add to role failed: {addToRoleResult?.Errors?.FirstOrDefault()?.Description}", Success = false };

        return new RegisterResponse
        {
            Success = true,
            Message = "User registered successfully"
        };
    }
    catch (Exception ex)
    {
        return new RegisterResponse { Message = ex.Message, Success = false };
    }
}


        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null) return new LoginResponse { Message = "Invalid email/password", Success = false };

                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email.ToString()),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var roles = await _userManager.GetRolesAsync(user);
                var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
                claims.AddRange(roleClaims);

                // var keyString = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
                var keyString = "486897e25269fb7777a0376bf91ffc4e5ec9e199f52daff5053659182e0c96342a204da9643914dc78d2e4020b0dca42286a0d3b9dd29a34bb897dc8c588a032";
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expires = DateTime.Now.AddMinutes(30);

                var token = new JwtSecurityToken(
                    issuer: "https://localhost:5000",
                    audience: "https://localhost:5000",
                    claims: claims,
                    expires: expires,
                    signingCredentials: creds
                );

                return new LoginResponse
                {
                    AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                    FullName = user?.FullName ?? "",
                    Email = user?.Email ?? "",
                    Success = true,
                    Id = user?.Id.ToString() ?? "",
                    Role = roles.FirstOrDefault() ?? ""
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new LoginResponse { Success = false, Message = ex.Message };
            }
        }

        public async Task<UserProfileResponse> GetMyProfileAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new UserProfileResponse { Success = false, Message = "User not found." };
                }

                return new UserProfileResponse
                {
                    Email = user.Email ?? "",
                    FullName = user.FullName,
                    Id = user.Id.ToString(),
                    Message = "User profile retrieved successfully",
                    Success = true
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UserProfileResponse { Success = false, Message = "An error occurred while processing your request." };
            }
        }

        public async Task<UpdateProfileResponse> UpdateProfileAsync(UpdateProfileRequest request, string userEmail)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userEmail);

                if (user == null)
                {
                    return new UpdateProfileResponse { Success = false, Message = "User not found." };
                }

                user.FullName = request.FullName;
                var result = await _userManager.UpdateAsync(user);

                return result.Succeeded
                    ? new UpdateProfileResponse { Success = true, Message = "Profile updated successfully" }
                    : new UpdateProfileResponse { Success = false, Message = "Profile update failed", Errors = result.Errors };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new UpdateProfileResponse { Success = false, Message = "An error occurred while processing your request." };
            }
        }

        public async Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                if (user == null)
                {
                    return new ChangePasswordResponse { Success = false, Message = "User not found." };
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                return result.Succeeded
                    ? new ChangePasswordResponse { Success = true, Message = "Password changed successfully" }
                    : new ChangePasswordResponse { Success = false, Message = "Password change failed", Errors = result.Errors };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ChangePasswordResponse { Success = false, Message = "An error occurred while processing your request." };
            }
        }
            public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
        {
            return await Task.FromResult(_userManager.Users.ToList());
        }
    }

}
