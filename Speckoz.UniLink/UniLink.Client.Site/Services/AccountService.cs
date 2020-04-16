using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.Client.Site.Services
{
	public class AccountService
	{
		public async Task<UserModel> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await new RequestService()
			{
				Protocol = Protocols.HTTP,
				URL = DataHelper.URLBase,
				URN = "Auth",
				Method = Method.POST,
				Body = login
			}.ExecuteTaskAsync();

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<UserModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return null;
		}

		public async Task<UserModel> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await new RequestService()
			{
				Protocol = Protocols.HTTP,
				URL = DataHelper.URLBase,
				URN = "Auth/User",
				Method = Method.POST,
				Body = new { Email = login }
			}.ExecuteTaskAsync();

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<UserModel>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return null;
		}
	}
}