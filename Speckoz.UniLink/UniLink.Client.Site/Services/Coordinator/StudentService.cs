using Blazored.SessionStorage;

using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO.Student;

namespace UniLink.Client.Site.Services.Coordinator
{
	public class StudentService
	{
		private readonly ISessionStorageService _sessionStorage;

		public StudentService(ISessionStorageService sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}

		public async Task<StudentDisciplineVO> AddStudentTaskAsync(StudentVO student)
		{
			IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), student);

			if (response.StatusCode == HttpStatusCode.Created)
				return JsonSerializer.Deserialize<StudentDisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return default;

			static async Task<IRestResponse> SendRequestTaskAsync(string token, StudentVO student)
			{
				return await new RequestService()
				{
					Method = Method.POST,
					URL = DataHelper.URLBase,
					URN = $"Students",
					Body = student,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<IList<StudentDisciplineVO>> GetStudentsTaskAsync(Guid courseId)
		{
			IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), courseId);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<StudentDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else if (response.StatusCode == HttpStatusCode.NotFound)
				return new List<StudentDisciplineVO>();

			return default;

			async Task<IRestResponse> SendRequestTaskAsync(string token, Guid courseId)
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Students/{courseId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}