using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Services.Coordinator
{
	public class CourseService
	{
		public async Task<ResultModel<CourseVO>> AddCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			return resp.StatusCode switch
			{
				HttpStatusCode.Created => new ResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "Curso criado com sucesso!"
				},

				_ => new ResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				}
			};
			async Task<IRestResponse> SendRequestTaskAsync(CourseVO newCourse, string token) => await new RequestService()
			{
				Method = Method.POST,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Body = newCourse,
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}

		public async Task<ResultModel<CourseVO>> GetCourseByCoordIdTaskAsync(string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(token);

			return resp.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "Sucesso!"
				},

				_ => new ResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				},
			};
			async Task<IRestResponse> SendRequestTaskAsync(string token) => await new RequestService()
			{
				Method = Method.GET,
				URL = DataHelper.URLBase,
				URN = "Courses",
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}

		public async Task<ResultModel<CourseVO>> UpdateCourseTaskAsync(CourseVO newCourse, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newCourse, token);

			return resp.StatusCode switch
			{
				HttpStatusCode.OK => new ResultModel<CourseVO>
				{
					Object = JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }),
					StatusCode = resp.StatusCode,
					Message = "As informaçoes foram atualizadas com sucesso!"
				},

				_ => new ResultModel<CourseVO>
				{
					StatusCode = resp.StatusCode,
					Message = resp.Content.Replace("\"", string.Empty)
				},
			};
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