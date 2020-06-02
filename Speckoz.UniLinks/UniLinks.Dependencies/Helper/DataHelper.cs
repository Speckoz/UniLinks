using static System.Environment;

namespace UniLinks.Dependencies.Helper
{
	public class DataHelper
	{
		public static string URLBase = GetEnvironmentVariable("API_URL") ?? "http://localhost:5050/api";
	}
}