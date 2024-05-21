using MongoAuthenticatorAPI.Dtos;
using System.Threading.Tasks;

namespace MongoAuthenticatorAPI.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<UserProfileResponse> GetMyProfileAsync(string userId);
        Task<UpdateProfileResponse> UpdateProfileAsync(UpdateProfileRequest request, string userEmail);
        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request, string userId);
    }
}
