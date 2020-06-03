using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Student
{
	public class LessonService
	{
		public async Task<ResponseModel<List<LessonDisciplineVO>>> GetAllLessonsTaskAync(string token, List<string> disciplines)
		{
			IRestResponse response = await SendRequestTaskAsync(token, disciplines);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResponseModel<List<LessonDisciplineVO>>
				{
					Object = JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = response.StatusCode,
					Message = "Sucesso!"
				},

				_ => new ResponseModel<List<LessonDisciplineVO>>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

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