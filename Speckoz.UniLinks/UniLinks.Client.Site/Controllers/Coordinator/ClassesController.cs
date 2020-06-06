using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	public class ClassesController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index([FromServices] ClassService classService)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<List<ClassVO>> response = await classService.GetClassesTaskAsync(token);

			return View("/Views/Coordinator/Classes/Index.cshtml", response.Object);
		}
	}
}