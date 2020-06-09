using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Coordinator
{
	public class StatusService
	{
		public async Task<ResultModel<StatusVO>> GetStatusDataTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<StatusVO>
				{
					Object = JsonSerializer.Deserialize<StatusVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StatusVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = "Status",
					Authenticator = new JwtAuthenticator(token),
				}.ExecuteTaskAsync();
			}
		}
	}
}