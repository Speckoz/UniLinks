using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;

namespace UniLinks.Client.Web.Services.Coordinator
{
	public class CourseService
	{
		public async Task<CourseVO> AddCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			if (resp.StatusCode == HttpStatusCode.Created)
				return JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(CourseVO newCourse, string token) => await new RequestService()
			{
				Method = Method.POST,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Body = newCourse,
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}

		public async Task<CourseVO> GetCourseByCoordIdTaskAsync(string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token) => await new RequestService()
			{
				Method = Method.GET,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}

		public async Task<CourseVO> UpdateCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(CourseVO newCourse, string token) => await new RequestService()
			{
				Method = Method.PUT,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Body = newCourse,
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}
	}
}