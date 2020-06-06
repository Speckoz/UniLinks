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
		public async Task<ResultModel<StudentVO>> AddStudentTaskAsync(AuthStudentVO student, string token, Guid courseId)
		{
			student.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<StudentVO>
				{
					Object = JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
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

		public async Task<ResultModel<StudentVO>> GetStudentTaskAsync(string token, Guid studentId)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<StudentVO>
				{
					Object = JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Students/{studentId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<List<StudentVO>>> GetStudentsTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<List<StudentVO>>
				{
					Object = JsonSerializer.Deserialize<List<StudentVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Aluno removido com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<List<StudentVO>>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
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

		public async Task<ResultModel<StudentVO>> UpdateStudentTaskAsync(StudentVO newStudent, string token, Guid courseId)
		{
			newStudent.CourseId = courseId;
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.NoContent => new ResultModel<StudentVO>
				{
					Object = JsonSerializer.Deserialize<StudentVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentVO>
				{
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = $"Students",
					Body = newStudent,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<bool>> RemoveStudentTaskAsync(Guid studentId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.NoContent => new ResultModel<bool>
				{
					Object = true,
					Message = "Aluno removido com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<bool>
				{
					Object = false,
					Message = response.Content.Replace("\"", string.Empty),
					StatusCode = response.StatusCode
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
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