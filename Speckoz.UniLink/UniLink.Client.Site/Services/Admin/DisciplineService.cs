using Dependencies.Services;

using RestSharp;
using RestSharp.Authenticators;

using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Dependencies.Data.VO;

namespace UniLink.Client.Site.Services.Admin
{
	public class DisciplineService
	{
		public async Task<IList<DisciplineVO>> GetDisciplinesByRangeTaskAsync(string token, string disciplines)
		{
			IRestResponse response = await SendRequestTaskAsync(token, disciplines);

			if (response.StatusCode == HttpStatusCode.OK)
				return JsonSerializer.Deserialize<List<DisciplineVO>>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			else
				return new List<DisciplineVO>();
		}

		private async Task<IRestResponse> SendRequestTaskAsync(string token, string disciplines) => await new RequestService()
		{
			Method = Method.GET,
			URL = DataHelper.URLBase,
			URN = $"Disciplines/{disciplines}",
			Authenticator = new JwtAuthenticator(token)
		}.ExecuteTaskAsync();
	}
}