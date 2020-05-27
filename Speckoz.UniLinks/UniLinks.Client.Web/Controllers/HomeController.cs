using Microsoft.AspNetCore.Mvc;

namespace UniLinks.Client.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();
	}
}