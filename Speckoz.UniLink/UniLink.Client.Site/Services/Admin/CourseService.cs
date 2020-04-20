﻿using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO;

namespace UniLink.Client.Site.Services.Admin
{
	public class CourseService
	{
		public async Task<CourseVO> GetCourseByCoordIdTaskAsync(string token)
		{
			IRestResponse resp = await SendRequestTaskAsync(token);

			if (resp.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<CourseVO>(resp.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

			return null;
		}

		private async Task<IRestResponse> SendRequestTaskAsync(string token) => await new RequestService()
		{
			Method = Method.GET,
			URL = DataHelper.URLBase,
			URN = $"Courses",
			Authenticator = new JwtAuthenticator(token)
		}.ExecuteTaskAsync();
	}
}