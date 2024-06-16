public class ChangePasswordResponse
{
  public string Id { get; set; }
  public bool Success { get; set; }
  public string Message { get; set; }
  public string FullName { get; set; }
  public object Errors { get; set; }
}
