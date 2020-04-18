using Microsoft.AspNetCore.Components.Authorization;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLink.Dependencies.Enums;

namespace UniLink.Client.Site.Services
{
	public class AuthenticationStateProviderService : AuthenticationStateProvider
	{
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
				new Claim(ClaimTypes.Role, UserTypeEnum.Coordinator.ToString())
			}, "apiauth_type");

			var user = new ClaimsPrincipal(identity);

			return await Task.FromResult(new AuthenticationState(user));
		}
	}
}