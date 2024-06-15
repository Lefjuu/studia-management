using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleAuthorizeAttribute : TypeFilterAttribute
{
    public RoleAuthorizeAttribute(string role) : base(typeof(RoleAuthorizeFilter))
    {
        Arguments = new object[] { role };
    }
}

public class RoleAuthorizeFilter : IAuthorizationFilter
{
    private readonly string _role;

    public RoleAuthorizeFilter(string role)
    {
        _role = role;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roles = context.HttpContext.Items["UserRoles"] as IList<string>;
        if (roles == null || !roles.Contains(_role))
        {
            context.Result = new ForbidResult();
        }
    }
}
