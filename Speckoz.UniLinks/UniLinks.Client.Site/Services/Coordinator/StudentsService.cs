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
		public async Task<ResultModel<StudentDisciplineVO>> AddStudentTaskAsync(StudentVO student, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<StudentDisciplineVO>
				{
					Object = JsonSerializer.Deserialize<StudentDisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Aluno adicionado com Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentDisciplineVO>
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

		public async Task<ResultModel<StudentDisciplineVO>> GetStudentTaskAsync(string token, Guid studentId)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<StudentDisciplineVO>
				{
					Object = JsonSerializer.Deserialize<StudentDisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentDisciplineVO>
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

		public async Task<ResultModel<List<StudentDisciplineVO>>> GetStudentsTaskAsync(string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<List<StudentDisciplineVO>>
				{
					Object = JsonSerializer.Deserialize<List<StudentDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "Sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<List<StudentDisciplineVO>>
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

		public async Task<ResultModel<StudentDisciplineVO>> UpdateStudentTaskAsync(StudentVO newStudent, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<StudentDisciplineVO>
				{
					Object = JsonSerializer.Deserialize<StudentDisciplineVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					Message = "As informaçoes foram atualizadas com sucesso!",
					StatusCode = response.StatusCode
				},

				_ => new ResultModel<StudentDisciplineVO>
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