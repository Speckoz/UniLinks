using Blazored.LocalStorage;

using Microsoft.AspNetCore.Components.Server;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Services.Interfaces;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Services
{
	public class AuthService : IAuthService
	{
		private readonly HttpClient _httpClient;
		private readonly ServerAuthenticationStateProvider _authenticationStateProvider;
		private readonly ILocalStorageService _localStorage;

		public AuthService(HttpClient httpClient, ServerAuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_authenticationStateProvider = authenticationStateProvider;
			_localStorage = localStorage;
		}

		public async Task<UserModel> AuthTaskAsync(LoginRequestModel loginModel)
		{
			await _authenticationStateProvider.GetAuthenticationStateAsync();

			string loginAsJson = JsonSerializer.Serialize(loginModel);
			HttpResponseMessage response = await _httpClient.PostAsync("http://localhost:5050/api/auth", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
			UserModel loginResult = JsonSerializer.Deserialize<UserModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			if (!response.IsSuccessStatusCode)
				return loginResult;

			await _localStorage.SetItemAsync("authToken", loginResult.Token);
			((AuthenticationStateProviderService) _authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

			return loginResult;
		}

		public async Task LogoutAsync()
		{
			await _localStorage.RemoveItemAsync("authToken");
			((AuthenticationStateProviderService) _authenticationStateProvider).MarkUserAsLoggedOut();
			_httpClient.DefaultRequestHeaders.Authorization = null;
		}
	}
}