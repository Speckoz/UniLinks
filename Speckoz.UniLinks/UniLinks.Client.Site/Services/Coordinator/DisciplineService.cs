using Blazored.SessionStorage;

using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;

namespace UniLinks.Client.Site.Services.Coordinator
{
	public class DisciplineService
	{
		private readonly ISessionStorageService _sessionStorage;

		public DisciplineService(ISessionStorageService sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}

		public async Task<List<DisciplineVO>> GetDisciplinesByCoordIdTaskAsync()
		{
			string token = await _sessionStorage.GetItemAsync<string>("token");

			IRestResponse response = await SendRequestTaskAsync(token);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<DisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return new List<DisciplineVO>();

			static async Task<IRestResponse> SendRequestTaskAsync(string token)
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Disciplines",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}