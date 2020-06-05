using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class DisciplinesController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index([FromServices] DisciplineService disciplineService)
		{
			string token = User.FindFirst("Token").Value;

			ResponseModel<List<DisciplineVO>> response = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Disciplines/Index.cshtml", response.Object);
		}
	}
}