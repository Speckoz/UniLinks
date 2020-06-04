using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Enums;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	public class LessonsController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> Index([FromServices] LessonService lessonService, [FromServices] DisciplineService disciplineService)
		{
			string token = User.FindFirst("Token").Value;

			List<DisciplineVO> disciplines = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);
			List<LessonDisciplineVO> lessons = await lessonService.GetAllLessonsByDisciplineIDsTaskAsync(token, disciplines.Select(x => x.DisciplineId).ToList());

			return View("/Views/Coordinator/Lessons/Index.cshtml", lessons);
		}
	}
}