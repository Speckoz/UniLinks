using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Lesson;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class LessonsController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index([FromServices] LessonService lessonService, [FromServices] DisciplineService disciplineService)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<List<DisciplineVO>> disciplines = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);
			ResultModel<List<LessonDisciplineVO>> lessons = await lessonService.GetAllLessonsByDisciplineIDsTaskAsync(token, disciplines.Object.Select(x => x.DisciplineId).ToList());

			return View("/Views/Coordinator/Lessons/Index.cshtml", lessons);
		}

		[HttpGet("Add")]
		public IActionResult Add() => View("/Views/Coordinator/Lessons/Add.cshtml");

		[HttpPost]
		public async Task<IActionResult> AddLesson([FromServices] LessonService lessonService, [FromServices] DisciplineService disciplineService, ResultModel<LessonVO> request)
		{
			string token = User.FindFirst("Token").Value;

			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<LessonVO> response = await lessonService.AddLessonTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Lessons/Add.cshtml", new ResultModel<LessonVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<DisciplineVO>> disciplines = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);
			ResultModel<List<LessonDisciplineVO>> lessonsResponse = await lessonService.GetAllLessonsByDisciplineIDsTaskAsync(token, disciplines.Object.Select(x => x.DisciplineId).ToList());

			if (lessonsResponse.StatusCode == HttpStatusCode.OK)
			{
				lessonsResponse.Message = response.Message;
				lessonsResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Lessons/Index.cshtml", lessonsResponse);
		}

		[HttpPost("Delete/{lessonId}")]
		public async Task<IActionResult> Delete([FromServices] LessonService lessonService, [FromServices] DisciplineService disciplineService, [Required] Guid lessonId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<bool> response = await lessonService.RemoveLessonTaskAsync(lessonId, token);

				ResultModel<List<DisciplineVO>> disciplines = await disciplineService.GetDisciplinesByCoordIdTaskAsync(token);
				ResultModel<List<LessonDisciplineVO>> lessons = await lessonService.GetAllLessonsByDisciplineIDsTaskAsync(token, disciplines.Object.Select(x => x.DisciplineId).ToList());

				if (disciplines.StatusCode == HttpStatusCode.OK && lessons.StatusCode == HttpStatusCode.OK)
				{
					lessons.StatusCode = response.StatusCode;
					lessons.Message = response.Message;
				}

				return View("/Views/Coordinator/Lessons/Index.cshtml", lessons);
			}

			return BadRequest();
		}
	}
}