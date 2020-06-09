using System.Net;

namespace UniLinks.Dependencies.Models
{
	public class ResultModel<T>
	{
		public T Object { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public string Message { get; set; }
	}
}