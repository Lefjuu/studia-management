// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Options;
// using MongoDB.Driver;
// using AspNetCore.Identity.MongoDbCore.Models;
// using AspNetCore.Identity.MongoDbCore.Infrastructure;
// using AspNetCore.Identity.MongoDbCore.Extensions;
// using System;
// using System.Linq;
// using System.Security.Claims;
// using System.Threading.Tasks;
// using System.IdentityModel.Tokens.Jwt;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;
// using AspNetCore.Identity.MongoDbCore;

// public class AuthService
// {
//     private readonly UserManager<User> _userManager;
//     private readonly SignInManager<User> _signInManager;
//     private readonly IConfiguration _configuration;

//     public AuthService(
//         IOptions<MongoDbSettings> settings,
//         UserManager<User> userManager,
//         SignInManager<User> signInManager,
//         IConfiguration configuration)
//     {
//         var client = new MongoClient(settings.Value.ConnectionString);
//         var database = client.GetDatabase(settings.Value.DatabaseName);

//         var identityOptions = new MongoDbIdentityConfiguration
//         {
//             MongoDbSettings = new AspNetCore.Identity.MongoDbCore.Infrastructure.MongoDbSettings
//             {
//                 ConnectionString = settings.Value.ConnectionString,
//                 DatabaseName = settings.Value.DatabaseName
//             },
//             IdentityOptionsAction = options =>
//             {
//                 options.Password.RequiredLength = 8;
//                 options.Password.RequireLowercase = false;
//                 options.Password.RequireUppercase = false;
//                 options.Password.RequireNonAlphanumeric = false;
//                 options.Password.RequireDigit = false;
//             }
//         };

//         var userStore = new MongoUserStore<User>(database, identityOptions);
//         _userManager = userManager;
//         _signInManager = signInManager;
//         _configuration = configuration;
//     }

//     public async Task<(bool, string)> RegisterUser(RegisterModel model)
//     {
//         var userExists = await _userManager.FindByEmailAsync(model.Email);
//         if (userExists != null)
//         {
//             return (false, "User already exists!");
//         }

//         User user = new User
//         {
//             Email = model.Email,
//             SecurityStamp = Guid.NewGuid().ToString(),
//             Name = model.Name
//         };

//         var result = await _userManager.CreateAsync(user, model.Password);
//         if (result.Succeeded)
//         {
//             return (true, "User created successfully!");
//         }

//         return (false, string.Join(", ", result.Errors.Select(x => x.Description)));
//     }

//     public async Task<(bool, string, string)> ValidateUserAsync(LoginModel model)
//     {
//         var user = await _userManager.FindByEmailAsync(model.Email);
//         if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//         {
//             var token = GenerateJwtToken(user);
//             return (true, token, string.Empty);
//         }
//         return (false, null, "Invalid credentials");
//     }

//     private string GenerateJwtToken(User user)
//     {
//         var authClaims = new[]
//         {
//             new Claim(ClaimTypes.Name, user.Email),
//             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//         };

//         var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
//         var token = new JwtSecurityToken(
//             issuer: _configuration["JWT:ValidIssuer"],
//             audience: _configuration["JWT:ValidAudience"],
//             expires: DateTime.Now.AddHours(3),
//             claims: authClaims,
//             signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//         );

//         return new JwtSecurityTokenHandler().WriteToken(token);
//     }
// }
