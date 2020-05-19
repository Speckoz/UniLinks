using Microsoft.AspNetCore.Mvc;

namespace UniLink.Client.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index() => View();
	}
}