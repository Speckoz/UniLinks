using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	public class StudentsController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> Index([FromServices] StudentsService studentsService)
		{
			string token = User.FindFirst("Token").Value;

			List<StudentDisciplineVO> students = await studentsService.GetStudentsTaskAsync(token);
			return View("/Views/Coordinator/Students/Index.cshtml", students);
		}
	}
}