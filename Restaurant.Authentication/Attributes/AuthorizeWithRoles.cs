using Microsoft.AspNetCore.Authorization;
using Restaurant.Entities.Enums;

namespace Restaurant.Authentication.Attributes
{
    public class AuthorizeWithRoles : AuthorizeAttribute
    {
        public AuthorizeWithRoles(params RoleEnum[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
