using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Helper;

namespace UniLinks.Client.Web.Services.Coordinator
{
	public class ClassService
	{
		public async Task<ClassVO> AddClasseTaskAsync(ClassVO newClass, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newClass, token);

			if (resp.StatusCode == HttpStatusCode.Created)
				return JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(ClassVO newClass, string token)
			{
				return await new RequestService()
				{
					Method = Method.POST,
					URL = DataHelper.URLBase,
					URN = "Classes",
					Body = newClass,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ClassVO> GetClassesTaskAsync(string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(string token)
			{
				return await new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = "Classes",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<ClassVO> GetClassesByCourseIdAndPeriodTaskAsync(Guid courseId, int period, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(courseId, period.ToString(), token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(Guid courseId, string period, string token)
			{
				var request = new RequestService()
				{
					Method = Method.GET,
					URL = DataHelper.URLBase,
					URN = $"Classes/{courseId}",
					Authenticator = new JwtAuthenticator(token)
				};
				request.Parameters.Add("period", period);
				return await request.ExecuteTaskAsync();
			}
		}

		public async Task<ClassVO> UpdateClassTaskAsync(ClassVO newClass, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(newClass, token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<ClassVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;

			async Task<IRestResponse> SendRequestTaskAsync(ClassVO newClass, string token)
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = "Classes",
					Body = newClass,
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}

		public async Task<bool> RemoveClassTaskAsync(Guid classId, string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(classId, token);

			return resp.StatusCode == HttpStatusCode.NoContent;

			async Task<IRestResponse> SendRequestTaskAsync(Guid classId, string token)
			{
				return await new RequestService()
				{
					Method = Method.PUT,
					URL = DataHelper.URLBase,
					URN = $"Classes/{classId}",
					Authenticator = new JwtAuthenticator(token)
				}.ExecuteTaskAsync();
			}
		}
	}
}