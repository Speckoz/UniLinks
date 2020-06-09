using Dependencies.Services;

using RestSharp;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Class;

namespace UniLinks.API.Services
{
	public class CollabAPIService
	{
		public async Task<LessonVO> GetRecordingInfoTaskAsync(LessonVO lesson)
		{
			string[] parts = lesson.URI.Split('/');
			if (parts.Length <= 4)
				return null;

			IRestResponse response = await SendRequestTaskAsync();

			if (response.StatusCode == HttpStatusCode.OK)
			{
				LessonVO lessonCollab = JsonSerializer.Deserialize<LessonVO>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

				lesson.RecordName = lessonCollab.RecordName;
				lesson.Duration = lessonCollab.Duration;
				lesson.Date = lessonCollab.Date;

				return lesson;
			}

			return null;

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = "https://us.bbcollab.com",
					URN = $"collab/api/csa/recordings/{parts[4]}/data",
				}.ExecuteTaskAsync();
			}
		}

		public async Task<bool> GetClassInfoTaskAsync(ClassVO @class)
		{
			string[] parts = @class.URI.Split('/');
			if (parts.Length <= 4)
				return false;

			IRestResponse response = await SendRequestTaskAsync();

			return response.StatusCode == HttpStatusCode.Forbidden;

			async Task<IRestResponse> SendRequestTaskAsync()
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = "https://us.bbcollab.com",
					URN = $"collab/api/guest/{parts[4]}",
				}.ExecuteTaskAsync();
			}
		}
	}
}