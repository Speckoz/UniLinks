using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Services
{
	public class AuthService
	{
		public async Task<AuthCoordinatorVO> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await SendRequestTaskAsync(login, "Auth/coordinator");

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonSerializer.Deserialize<AuthCoordinatorVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}

			return null;
		}

		public async Task<StudentVO> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await SendRequestTaskAsync(new { Email = login }, "Auth/student");

			if (response.StatusCode == HttpStatusCode.OK)
			{
				return JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			}

			return null;
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