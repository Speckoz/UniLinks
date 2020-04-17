using Microsoft.AspNetCore.Authorization;

using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Attributes
{
	public class AuthorizesAttribute : AuthorizeAttribute
	{
		public AuthorizesAttribute(params UserTypeEnum[] roles) : base() => Roles = string.Join(",", roles);
	}
}