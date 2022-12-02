using Microsoft.AspNetCore.Authorization;
using Restaurant.Entities.Enums;

namespace Restaurant.APIComponents.Attributes
{
    public class AuthorizeWithRoles : AuthorizeAttribute
    {
        public AuthorizeWithRoles(params RoleEnum[] roles)
        {
            base.Roles = string.Join(",", roles);
        }
    }
}
