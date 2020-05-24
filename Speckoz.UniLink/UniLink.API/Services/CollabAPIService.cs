using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Services
{
	public class CollabAPIService
	{
		public async Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson)
		{
			IRestResponse response = await SendRequestTaskAsync(lesson.URI.Split('/')[4]);

			if (response.StatusCode == HttpStatusCode.OK)
			{
				LessonVO lessonCollab = JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				lesson.RecordName = lessonCollab.RecordName;
				lesson.Duration = lessonCollab.Duration;
				lesson.Date = lessonCollab.Date;

				return lesson;
			}

			return null;

			static async Task<IRestResponse> SendRequestTaskAsync(string id)
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = "us.bbcollab.com",
					URN = $"collab/api/csa/recordings/{id}/data",
				}.ExecuteTaskAsync();
			}
		}
	}
}