using Dependencies.Services;

using Microsoft.AspNetCore.Components.Authorization;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO.Coordinator;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Services
{
	public class AccountService
	{
		private readonly AuthenticationStateProvider _authentication;

		public AccountService(AuthenticationStateProvider authentication) =>
			_authentication = authentication;

		public async Task<bool> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await SendRequestTaskAsync(login, "Auth");

			if (response.StatusCode == HttpStatusCode.OK)
			{
				AuthCoordinatorVO coord = JsonSerializer.Deserialize<AuthCoordinatorVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				await ((AuthenticationStateProviderService) _authentication).MarkUserWithAuthenticatedAsync(coord);
				return true;
			}

			return false;
		}

		public async Task<bool> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await SendRequestTaskAsync(new { Email = login }, "Auth/User");

			if (response.StatusCode == HttpStatusCode.OK)
			{
				StudentVO student = JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
				await ((AuthenticationStateProviderService) _authentication).MarkUserWithAuthenticatedAsync(student);
				return true;
			}

			return false;
		}

		private async Task<IRestResponse> SendRequestTaskAsync(object body, string urn)
		{
			return await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = urn,
				Method = Method.POST,
				Body = body
			}.ExecuteTaskAsync();
		}
	}
}