using Blazored.SessionStorage;

using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO.Lesson;

namespace UniLink.Client.Site.Services.Student
{
    public class LessonService
    {
        private readonly ISessionStorageService _sessionStorage;

        public LessonService(ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async Task<List<LessonDisciplineVO>> GetAllLessonsTaskAync()
        {
            IRestResponse response = await SendRequestTaskAsync(
                await _sessionStorage.GetItemAsync<string>("token"),
                await _sessionStorage.GetItemAsync<string>("disciplines"));

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<List<LessonDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;
        }

        private async Task<IRestResponse> SendRequestTaskAsync(string token, string disciplines)
        {
            return await new RequestService()
            {
                Method = Method.GET,
                URL = DataHelper.URLBase,
                URN = $"lessons/all/{disciplines}",
                Authenticator = new JwtAuthenticator(token)
            }.ExecuteTaskAsync();
        }
    }
}