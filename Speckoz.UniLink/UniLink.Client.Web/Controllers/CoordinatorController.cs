using Microsoft.AspNetCore.Mvc;

namespace UniLink.Client.Web.Controllers
{
	public class CoordinatorController : Controller
	{
		public IActionResult IndexCoordinator() => View();
	}
}