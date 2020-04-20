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
			IRestResponse response = await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Auth",
				Method = Method.POST,
				Body = login
			}.ExecuteTaskAsync();

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<CoordinatorVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return null;
		}

		public async Task<StudentVO> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await new RequestService()
			{
				URL = DataHelper.URLBase,
				URN = "Auth/User",
				Method = Method.POST,
				Body = new { Email = login }
			}.ExecuteTaskAsync();

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return null;
		}
	}
}