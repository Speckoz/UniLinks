using Microsoft.AspNetCore.Authorization;

using UniLinks.Dependencies.Enums;

namespace UniLinks.Dependencies.Attributes
{
    public class AuthorizesAttribute : AuthorizeAttribute
    {
        public AuthorizesAttribute(params UserTypeEnum[] roles) : base()
        {
            if (roles.Length == 0)
                return;

            Roles = string.Join(",", roles);
        }
    }
}