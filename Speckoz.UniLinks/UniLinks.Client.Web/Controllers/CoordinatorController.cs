using Microsoft.AspNetCore.Mvc;

namespace UniLinks.Client.Web.Controllers
{
	public class CoordinatorController : Controller
	{
		public IActionResult IndexCoordinator() => View();
	}
}