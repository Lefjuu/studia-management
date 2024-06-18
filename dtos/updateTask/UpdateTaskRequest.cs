namespace MongoAuthenticatorAPI.Dtos
{
    public class UpdateTaskRequest : TaskBaseRequest
    {
        public int Progress { get; set; }
    }
}
