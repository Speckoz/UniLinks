using Blazored.SessionStorage;
using Dependencies.Services;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UniLinks.Client.Site.Helper;
using UniLinks.Dependencies.Data.VO;

namespace UniLinks.Client.Site.Services.Coordinator
{
    public class LessonService
    {
        private readonly ISessionStorageService _sessionStorage;

        public LessonService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<LessonVO> GetLessonByIdTaskAsync(Guid lessonId) 
        {
            IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), lessonId);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;

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

        public async Task<LessonVO> AddLessonTaskAsync(LessonVO lesson)
        {
            IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), lesson);

            if (response.StatusCode == HttpStatusCode.Created)
                return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;

            async Task<IRestResponse> SendRequestTaskAsync(string token, LessonVO lesson)
            {
                return await new RequestService()
                {
                    Method = Method.POST,
                    URL = DataHelper.URLBase,
                    URN = $"lessons",
                    Body = lesson,
                    Authenticator = new JwtAuthenticator(token)
                }.ExecuteTaskAsync();
            }
        }

        public async Task<LessonVO> UpdateLessonTaskAsync(LessonVO lesson)
        {
            IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), lesson);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;

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

        public async Task<bool> RemoveLessonTaskAsync(Guid lessonId)
        {
            IRestResponse response = await SendRequestTaskAsync(await _sessionStorage.GetItemAsync<string>("token"), lessonId);

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
