using UniLink.Dependencies.Enums;

namespace UniLink.API.Attributes
{
	public class AuthorizesAttribute : Microsoft.AspNetCore.Authorization.AuthorizeAttribute
	{
		public AuthorizesAttribute(params UserTypeEnum[] roles) : base() => Roles = string.Join(",", roles);
	}
}