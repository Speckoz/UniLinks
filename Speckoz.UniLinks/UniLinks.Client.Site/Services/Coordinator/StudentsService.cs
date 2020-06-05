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
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Coordinator
{
	public class StudentsService
	{
		public async Task<ResponseModel<StudentDisciplineVO>> AddStudentTaskAsync(StudentVO student, string token, Guid courseId)
		{
			student.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync(token, student);

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResponseModel<StudentDisciplineVO>
				{
					Object = JsonSerializer.Deserialize<StudentDisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResponseModel<StudentDisciplineVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

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

		public async Task<ResponseModel<List<StudentDisciplineVO>>> GetStudentsTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResponseModel<List<StudentDisciplineVO>>
				{
					Object = JsonSerializer.Deserialize<List<StudentDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Aluno removido com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResponseModel<List<StudentDisciplineVO>>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

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

		public async Task<ResponseModel<StudentVO>> UpdateStudentTaskAsync(StudentVO newStudent, string token, Guid courseId)
		{
			newStudent.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync(token, newStudent);

			return response.StatusCode switch
			{
				HttpStatusCode.NoContent => new ResponseModel<StudentVO>
				{
					Object = JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResponseModel<StudentVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

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

		public async Task<ResponseModel<bool>> RemoveStudentTaskAsync(Guid studentId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, studentId);

			return response.StatusCode switch
			{
				HttpStatusCode.NoContent => new ResponseModel<bool>
				{
					Object = true,
					Message = "Aluno removido com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResponseModel<bool>
				{
					Object = false,
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

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