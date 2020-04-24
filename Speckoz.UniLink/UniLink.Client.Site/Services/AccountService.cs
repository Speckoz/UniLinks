using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Services
{
	public class AccountService
	{
		public async Task<CoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await SendRequestTaskAsync(login, "Auth");

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<CoordinatorVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return default;
		}

		public async Task<StudentVO> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await SendRequestTaskAsync(new { Email = login }, "Auth/User");

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return default;
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