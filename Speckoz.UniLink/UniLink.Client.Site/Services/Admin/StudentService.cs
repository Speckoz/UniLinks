using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Models;

namespace UniLink.Client.Site.Services.Admin
{
	public class StudentService
	{
		public async Task<IRestResponse> GetStudentsTaskAsync(string token, Guid courseId)
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