using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.Client.Site.Services
{
	public class AuthService
	{
		public async Task<ResultModel<AuthCoordinatorVO>> AuthAccountTaskAsync(LoginRequestModel login)
		{
			IRestResponse response = await SendRequestTaskAsync(login, "Auth/Coordinator");

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<AuthCoordinatorVO>
				{
					Object = JsonSerializer.Deserialize<AuthCoordinatorVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<AuthCoordinatorVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};
		}

		public async Task<ResultModel<AuthStudentVO>> AuthAccountTaskAsync(string login)
		{
			IRestResponse response = await SendRequestTaskAsync(new { Email = login }, "Auth/Student");

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<AuthStudentVO>
				{
					Object = JsonSerializer.Deserialize<AuthStudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<AuthStudentVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};
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