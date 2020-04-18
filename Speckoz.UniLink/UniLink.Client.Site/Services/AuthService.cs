using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using System.Threading.Tasks;

using UniLink.Client.Site.Services.Interfaces;

namespace UniLink.Client.Site.Services
{
	public class AuthService : IAuthService
	{
		private readonly IAuthorizationService _authorizationService;

		[CascadingParameter]
		public Task<AuthenticationState> AuthState { get; set; }

		public AuthService(IAuthorizationService authorizationService) =>
			_authorizationService = authorizationService;

		public async Task AuthorizeAsync()
		{
		}
	}
}