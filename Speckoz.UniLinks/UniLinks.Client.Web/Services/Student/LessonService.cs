using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Helper;

namespace UniLinks.Client.Site.Services.Student
{
	public class LessonService
	{
		public async Task<List<LessonDisciplineVO>> GetAllLessonsTaskAync(string token, string disciplines)
		{
			var dis = disciplines.Split(';').ToList();

			IRestResponse response = await SendRequestTaskAsync(token, dis);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token, List<string> disciplines)
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
}