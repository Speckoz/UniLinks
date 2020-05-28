using Dependencies.Services;

using Microsoft.AspNetCore.Components.Authorization;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Client.Site.Helper;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Services
{
	public class AccountService
	{
		private readonly AuthenticationStateProvider _authentication;

		public AccountService(AuthenticationStateProvider authentication) =>
			_authentication = authentication;

		public async Task<bool> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await SendRequestTaskAsync(login, "Auth/coordinator");

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
			IRestResponse response = await SendRequestTaskAsync(new { Email = login }, "Auth/student");

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