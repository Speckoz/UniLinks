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
        public async Task<CollabVO> GetRecordingInfo(int recordId)
        {
            IRestResponse response = await SendRequestTaskAsync(recordId);

            if (response.StatusCode == HttpStatusCode.OK)
                return JsonSerializer.Deserialize<CollabVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return default;

            static async Task<IRestResponse> SendRequestTaskAsync(int id)
            {
                return await new RequestService()
                {
                    Method = Method.GET,
                    URL = "https://us.bbcollab.com/collab/api/csa/recordings/",
                    URN = $"{id}/data",
                }.ExecuteTaskAsync();
            }
        }
    }
}
