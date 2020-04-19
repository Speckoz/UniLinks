using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Threading.Tasks;

using UniLink.Client.Site.Helper;

namespace UniLink.Client.Site.Services.Admin
{
	public class CourseService
	{
		public async Task<IRestResponse> GetCourseByCoordIdTaskAsync(string token)
		{
			return await new RequestService()
			{
				Method = Method.GET,
				URL = DataHelper.URLBase,
				URN = $"Courses",
				Authenticator = new JwtAuthenticator(token)
			}.ExecuteTaskAsync();
		}
	}
}