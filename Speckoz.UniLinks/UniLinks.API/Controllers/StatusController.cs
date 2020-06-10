using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Enums;

namespace UniLinks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class StatusController : ControllerBase
	{
		private readonly ILessonBusiness _lessonBusiness;
		private readonly IDisciplineBusiness _disciplineBusiness;
		private readonly IClassBusiness _classBusiness;
		private readonly IStudentBusiness _studentBusiness;

		public StatusController(ILessonBusiness lessonBusiness, IDisciplineBusiness disciplineBusiness, IClassBusiness classBusiness, IStudentBusiness studentBusiness)
		{
			_lessonBusiness = lessonBusiness;
			_disciplineBusiness = disciplineBusiness;
			_classBusiness = classBusiness;
			_studentBusiness = studentBusiness;
		}

		[HttpGet]
		public async Task<IActionResult> GetStatusTaskAsync([FromServices] ICourseBusiness courseBusiness)
		{
			var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			if (!(await courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course))
				return NotFound("Você não possui um curso cadastrado!");

			int lessonCount = await _lessonBusiness.FindCountByCourseIdTaskAsync(course.CourseId);
			int disciplinesCount = await _disciplineBusiness.FindCountByCourseIdTaskAsync(course.CourseId);
			int classesCount = await _classBusiness.FindCountByCourseIdTaskAsync(course.CourseId);
			int studentCount = await _studentBusiness.FindCountByCourseIdTaskAsync(course.CourseId);

			List<LessonDisciplineVO> fiveLastLessons = await _lessonBusiness.FindFiveLastLessonsByCourseIdTaskAsync(course.CourseId);

			return Ok(new StatusVO
			{
				ClassesCouunt = classesCount,
				DisciplinesCount = disciplinesCount,
				LessonsCount = lessonCount,
				StudentsCount = studentCount,
				FiveLastLessons = fiveLastLessons
			});
		}
	}
}