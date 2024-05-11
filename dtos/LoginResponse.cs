using System;
namespace MongoAuthenticatorAPI.Dtos
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}

