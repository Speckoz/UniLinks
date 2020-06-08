using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Coordinator
{
	public class LessonService
	{
		public async Task<ResultModel<LessonVO>> AddLessonTaskAsync(LessonVO lesson, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<LessonVO>
				{
					Object = JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = response.StatusCode,
					Message = "Aula adicionada com Sucessso!"
				},

				_ => new ResultModel<LessonVO>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.POST,
					URL = DataHelper.URLBase,
					URN = $"lessons/add",
					Body = lesson,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<LessonVO>> GetLessonByIdTaskAsync(Guid lessonId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<LessonVO>
				{
					Object = JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = response.StatusCode,
					Message = "Sucesso!"
				},

				_ => new ResultModel<LessonVO>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"lessons/{lessonId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<List<LessonDisciplineVO>>> GetAllLessonsByDisciplineIDsTaskAsync(string token, List<Guid> disciplines)
		{
			IRestResponse response = await SendRequestTaskAsync(token, disciplines);

			return response.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<List<LessonDisciplineVO>>
				{
					Object = JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = response.StatusCode,
					Message = "Sucessso!"
				},

				_ => new ResultModel<List<LessonDisciplineVO>>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

			static async Task<IRestResponse> SendRequestTaskAsync(string token, List<Guid> disciplines)
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

		public async Task<ResultModel<LessonVO>> UpdateLessonTaskAsync(LessonVO lesson, string token)
		{
			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<LessonVO>
				{
					Object = JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = response.StatusCode,
					Message = "As informaçoes da aula foram modificadas com Sucessso!"
				},

				_ => new ResultModel<LessonVO>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = $"lessons",
					Body = lesson,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ResultModel<bool>> RemoveLessonTaskAsync(Guid lessonId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, lessonId);

			return response.StatusCode switch
			{
				HttpStatusCode.NoContent => new ResultModel<bool>
				{
					Object = true,
					StatusCode = response.StatusCode,
					Message = "Aula removida com Sucessso!"
				},

				_ => new ResultModel<bool>
				{
					StatusCode = response.StatusCode,
					Message = response.Content.Replace("\"", string.Empty)
				}
			};

			static async Task<IRestResponse> SendRequestTaskAsync(string token, Guid lessonId)
			{
				return await new RequestService()
				{
					Method = Method.DELETE,
					URL = DataHelper.URLBase,
					URN = $"lessons/{lessonId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}