namespace MongoAuthenticatorAPI.Dtos
{
  public class TaskBaseRequest
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public string ProjectId { get; set; }
    public string UserId { get; set; }
  }
}
