using Dependencies.Services;
using RestSharp;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Services
{
    public class CollabAPIService
    {
        public async Task<CollabVO> GetRecordingInfo(string url)
        {
            string recordId = GetIDFromURL(url);
            IRestResponse response = await SendRequestTaskAsync(recordId);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<CollabVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;

            static async Task<IRestResponse> SendRequestTaskAsync(string id)
            {
                return await new RequestService()
                {
                    Method = Method.GET,
                    URL = "https://us.bbcollab.com/collab/api/csa/recordings/",
                    URN = $"{id}/data",
                }.ExecuteTaskAsync();
            }
        }
        private static string GetIDFromURL(string url) => url.Split('/')[4];
    }
}
