using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

public class RoleEnum
{
    public const string User = "user";
    public const string Admin = "admin";
}

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser<Guid>
{
    public string FullName { get; set; }
    public bool IsConnected { get; set; } = false;
    public DateTime LastModified { get; set; } = DateTime.Now;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public string Role { get; set; } = RoleEnum.User;
}
