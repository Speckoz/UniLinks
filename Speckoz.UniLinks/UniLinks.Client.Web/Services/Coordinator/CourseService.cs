using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Web.Services.Coordinator
{
	public class CourseService
	{
		public async Task<ResponseResultModel<CourseVO>> AddCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			if (resp.StatusCode == HttpStatusCode.Created)
			{
				return new ResponseResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "Curso criado com sucesso!"
				};
			}

			if (resp.StatusCode == HttpStatusCode.Conflict)
			{
				return new ResponseResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				};
			}

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

		public async Task<ResponseResultModel<CourseVO>> GetCourseByCoordIdTaskAsync(string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(token);

			if (resp.StatusCode == HttpStatusCode.OK)
			{
				return new ResponseResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "Sucesso!"
				};
			}

			if (resp.StatusCode == HttpStatusCode.NotFound)
			{
				return new ResponseResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				};
			}

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token) => await new RequestService()
			{
				Method = Method.GET,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}

		public async Task<ResponseResultModel<CourseVO>> UpdateCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			if (resp.StatusCode == HttpStatusCode.OK)
			{
				return new ResponseResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "As informaçoes foram atualizadas com sucesso!"
				};
			}

			if (resp.StatusCode == HttpStatusCode.Conflict)
			{
				return new ResponseResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				};
			}

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