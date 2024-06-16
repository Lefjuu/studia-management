using MongoAuthenticatorAPI.Dtos;

namespace MongoAuthenticatorAPI.Services
{
    public interface IAuthenticationService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
        Task<UserProfileResponse> GetMyProfileAsync(string userId);
        Task<UpdateProfileResponse> UpdateProfileAsync(UpdateProfileRequest request, string userEmail);
        Task<ChangePasswordResponse> ChangePasswordAsync(ChangePasswordRequest request, string userId);
        Task<IEnumerable<ApplicationUser>> GetUsersAsync();

    }
}
