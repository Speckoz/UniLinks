using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Helper;

namespace UniLinks.Client.Web.Services.Coordinator
{
	public class StudentsService
	{
		///
		public async Task<StudentDisciplineVO> AddStudentTaskAsync(StudentVO student, string token, Guid courseId)
		{
			student.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync(token, student);

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

		public async Task<List<StudentDisciplineVO>> GetStudentsTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<StudentDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return new List<StudentDisciplineVO>();

			async Task<IRestResponse> SendRequestTaskAsync(string token)
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Students/all",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<StudentVO> UpdateStudentTaskAsync(StudentVO newStudent, string token, Guid courseId)
		{
			newStudent.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync(token, newStudent);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			static async Task<IRestResponse> SendRequestTaskAsync(string token, StudentVO student)
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = $"Students",
					Body = student,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<bool> RemoveStudentTaskAsync(Guid studentId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, studentId);

			return response.StatusCode == HttpStatusCode.NoContent;

			static async Task<IRestResponse> SendRequestTaskAsync(string token, Guid studentId)
			{
				return await new RequestService()
				{
					Method = Method.DELETE,
					URL = DataHelper.URLBase,
					URN = $"Students/{studentId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}