using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace UniLink.Client.Site.Services
{
	public class AuthenticationStateProviderService : AuthenticationStateProvider
	{
		private readonly IConfiguration _configuration;

		public AuthenticationStateProviderService(IConfiguration configuration) =>
			_configuration = configuration;

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			ClaimsPrincipal user;
			try
			{
				user = ValidateToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRhNDE4NTYxLWUxYjAtNDFjOS05NTVkLTJiNDFmMzVmMGM0ZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkNvb3JkaW5hdG9yIiwiZXhwIjoxNTg3NDQ5NTc4LCJpc3MiOiJVbmlMaW5rIiwiYXVkIjoiVW5pTGluayJ9.wLt6cJyRmsBalzr_4UNj_wALvpNcrRfiPhgNS5OW_lE");
				//user = ValidateToken("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImRhNDE4NTYxLWUxYjAtNDFjOS05NTVkLTJiNDFmMzVmMGM0ZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkNvb3JkaW5hdG9yIiwiZXhwIjoxNTg3NDQ5NTc4LCJpc3MiOiJVbmlMaW5rIiwiYXVkIjoiVW5pTGluayJ9.wLt6cJyRmsBalzr_4UNj_wALvpNcrRfiPhgNS5OW_lE");
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

				ValidAudience = _configuration["JWT:Audience"],
				ValidIssuer = _configuration["JWT:Issuer"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]))
			};

			return new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out SecurityToken validatedToken);
		}
	}
}