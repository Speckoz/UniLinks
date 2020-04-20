using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Models;

namespace UniLink.Client.Site.Services.Admin
{
	public class StudentService
	{
		public async Task<IList<StudentModel>> GetStudentsTaskAsync(string token, Guid courseId)
		{
			IRestResponse response = await SendRequestTaskAsync(token, courseId);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<StudentModel>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else if (response.StatusCode == HttpStatusCode.NotFound)
				return new List<StudentModel>();

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