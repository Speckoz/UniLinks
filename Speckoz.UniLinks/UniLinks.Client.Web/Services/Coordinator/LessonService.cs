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

namespace UniLinks.Client.Web.Services.Coordinator
{
	public class LessonService
	{
		public async Task<LessonVO> AddLessonTaskAsync(LessonVO lesson, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, lesson);

			if (response.StatusCode == HttpStatusCode.Created)
				return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token, LessonVO lesson)
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

		public async Task<LessonVO> GetLessonByIdTaskAsync(Guid lessonId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, lessonId);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			static async Task<IRestResponse> SendRequestTaskAsync(string token, Guid lessonId)
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

		public async Task<List<LessonDisciplineVO>> GetAllLessonsByDisciplineIDsTaskAsync(string token, List<Guid> disciplines)
		{
			IRestResponse response = await SendRequestTaskAsync(token, disciplines);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

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

		public async Task<LessonVO> UpdateLessonTaskAsync(LessonVO lesson, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, lesson);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token, LessonVO lesson)
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

		public async Task<bool> RemoveLessonTaskAsync(Guid lessonId, string token)
		{
			IRestResponse response = await SendRequestTaskAsync(token, lessonId);

			return response.StatusCode == HttpStatusCode.NoContent;

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