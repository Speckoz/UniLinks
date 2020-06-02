using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UniLinks.Client.Web.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Enums;

namespace UniLinks.Client.Web.Controllers.Coordinator
{
	[Route("coordinator/course")]
	public class CourseController : Controller
	{
		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> Course([FromServices] CourseService courseService)
		{
			string token = User.FindFirst("Token").Value;

			CourseVO course = await courseService.GetCourseByCoordIdTaskAsync(token);

			return View("/Views/Coordinator/Course.cshtml", course);
		}
	}
}