using Dependencies;
using Dependencies.Services;

using RestSharp;

using System.Text.Json;
using System.Threading.Tasks;

using UniLink.Client.Site.Helper;
using UniLink.Client.Site.Models;

namespace UniLink.Client.Site.Services
{
    public class WeatherForecastService
    {
        public async Task<WeatherForecastModel[]> GetForecastAsync()
        {
            IRestResponse response = await new RequestService()
            {
                Protocol = Protocols.HTTP,
                URL = DataHelper.URLBase,
                URN = "WeatherForecast",
                Method = Method.GET
            }.ExecuteTaskAsync();

            return JsonSerializer.Deserialize<WeatherForecastModel[]>(response.Content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}