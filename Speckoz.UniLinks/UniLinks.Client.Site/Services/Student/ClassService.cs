using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Student
{
	public class ClassService
	{
		public async Task<ResultModel<List<ClassVO>>> GetAllClassesTaskAsync(string token)
		{
			IRestResponse response = await SendRequestAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<List<ClassVO>>
				{
					Object = JsonSerializer.Deserialize<List<ClassVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<List<ClassVO>>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = "classes/all/student",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}