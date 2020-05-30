using Blazored.SessionStorage;

using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Client.Site.Helper;
using UniLinks.Dependencies.Data.VO.Lesson;

namespace UniLinks.Client.Site.Services.Student
{
	public class LessonService
	{
		private readonly ISessionStorageService _sessionStorage;

		public LessonService(ISessionStorageService sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}

		public async Task<List<LessonDisciplineVO>> GetAllLessonsTaskAync()
		{
			var disciplines = await _sessionStorage.GetItemAsync<string>("disciplines");
			var dis = disciplines.Split(';').ToList();

			IRestResponse response = await SendRequestTaskAsync(
				await _sessionStorage.GetItemAsync<string>("token"), dis);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return default;
		}

		private async Task<IRestResponse> SendRequestTaskAsync(string token, List<string> disciplines)
		{
			return await new RequestService()
			{
				Method = Method.POST,
				URL = DataHelper.URLBase,
				URN = $"lessons",
				Body = disciplines,
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}
	}
}