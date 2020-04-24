using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;

namespace UniLink.Client.Site.Services.Coordinator
{
	public class StudentService
	{
		public async Task<IList<StudentDisciplineVO>> GetStudentsTaskAsync(string token, Guid courseId)
		{
			IRestResponse response = await SendRequestTaskAsync(token, courseId);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<StudentDisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else if (response.StatusCode == HttpStatusCode.NotFound)
				return new List<StudentDisciplineVO>();

			return null;
		}

		private async Task<IRestResponse> SendRequestTaskAsync(string token, Guid courseId)
		{
			return await new RequestService()
			{
				Method = Method.GET,
				URL = DataHelper.URLBase,
				URN = $"Students/{courseId}",
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}
	}
}