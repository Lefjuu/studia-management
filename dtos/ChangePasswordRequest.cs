    namespace MongoAuthenticatorAPI.Dtos {

    
    public class ChangePasswordRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
    }