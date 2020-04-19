using Blazored.SessionStorage;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using UniLink.Dependencies.Models;

namespace UniLink.Client.Site.Services
{
	public class AuthenticationStateProviderService : AuthenticationStateProvider
	{
		private readonly IConfiguration _configuration;
		private readonly ISessionStorageService _sessionStorage;
		private readonly NavigationManager _navigation;

		public AuthenticationStateProviderService(IConfiguration configuration, ISessionStorageService sessionStorage, NavigationManager navigation)
		{
			_configuration = configuration;
			_sessionStorage = sessionStorage;
			_navigation = navigation;
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			ClaimsPrincipal user;
			try
			{
				user = ValidateToken(await _sessionStorage.GetItemAsync<string>("token"));
			}
			catch
			{
				user = new ClaimsPrincipal();
			}

			return await Task.FromResult(new AuthenticationState(user));
		}

		public ClaimsPrincipal ValidateToken(string jwtToken)
		{
			IdentityModelEventSource.ShowPII = true;

			var validationParameters = new TokenValidationParameters
			{
				ValidateLifetime = true,
				RequireExpirationTime = true,

				ValidAudience = _configuration["JWT:Audience"],
				ValidIssuer = _configuration["JWT:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
			};

			return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
		}

		public async Task MarkUserWithAuthenticatedAsync(UserModel user)
		{
			await _sessionStorage.SetItemAsync("token", user.Token);
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(ValidateToken(user.Token))));
		}

		public async Task LogoutUserAsync()
		{
			_navigation.NavigateTo("/");
			await _sessionStorage.ClearAsync();
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
		}
	}
}