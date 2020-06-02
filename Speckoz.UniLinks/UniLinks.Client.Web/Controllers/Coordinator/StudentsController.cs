using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using System.Collections.Generic;
using System.Threading.Tasks;

using UniLinks.Client.Web.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;

namespace UniLinks.Client.Web.Controllers.Coordinator
{
	[Route("coordinator/students")]
	public class StudentsController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> Students([FromServices] StudentsService studentsService)
		{
			string token = User.FindFirst("Token").Value;

			List<StudentDisciplineVO> students = await studentsService.GetStudentsTaskAsync(token);
			return View("/Views/Coordinator/Students.cshtml", students);
		}
	}
}