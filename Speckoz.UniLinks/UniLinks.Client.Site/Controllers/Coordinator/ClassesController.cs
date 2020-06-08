using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
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

			return View("/Views/Coordinator/Classes/Index.cshtml", response);
		}

		[HttpGet("Add")]
		public async Task<IActionResult> Add([FromServices] CourseService courseService)
		{
			string token = User.FindFirst("Token").Value;

			ViewBag.periods = (await courseService.GetCourseByCoordIdTaskAsync(token)).Object.Periods;

			return View("/Views/Coordinator/Classes/Add.cshtml");
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddClass([FromServices] ClassService classService, ResultModel<ClassVO> request)
		{
			string token = User.FindFirst("Token").Value;
			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);
			ResultModel<ClassVO> response = await classService.AddClasseTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Classes/Add.cshtml", new ResultModel<ClassVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<ClassVO>> classResponse = await classService.GetClassesTaskAsync(token);

			if (classResponse.StatusCode == HttpStatusCode.OK)
			{
				classResponse.Message = response.Message;
				classResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Classes/Index.cshtml", classResponse);
		}

		[HttpGet("Update/{classId}")]
		public async Task<IActionResult> Update([FromServices] ClassService classService, [FromServices] CourseService courseService, [Required] Guid classId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ViewBag.periods = (await courseService.GetCourseByCoordIdTaskAsync(token)).Object.Periods;

				ResultModel<ClassVO> response = await classService.GetClassTaskAsync(classId, token);

				return View("/Views/Coordinator/Classes/Update.cshtml", response);
			}

			return BadRequest();
		}

		[HttpPost("Update")]
		public async Task<IActionResult> UpdateClass([FromServices] ClassService classService, ResultModel<ClassVO> request)
		{
			string token = User.FindFirst("Token").Value;
			request.Object.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<ClassVO> response = await classService.UpdateClassTaskAsync(request.Object, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/classes/Update.cshtml", new ResultModel<ClassVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<ClassVO>> classResponse = await classService.GetClassesTaskAsync(token);

			if (classResponse.StatusCode == HttpStatusCode.OK)
			{
				classResponse.Message = response.Message;
				classResponse.StatusCode = response.StatusCode;
			}

			return View("/Views/Coordinator/Classes/Index.cshtml", classResponse);
		}

		[HttpPost("Delete/{classId}")]
		public async Task<IActionResult> Delete([FromServices] ClassService classService, [Required] Guid classId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<bool> response = await classService.RemoveClassTaskAsync(classId, token);
				ResultModel<List<ClassVO>> studentResponse = await classService.GetClassesTaskAsync(token);

				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;

				return View("/Views/Coordinator/Classes/Index.cshtml", studentResponse);
			}

			return NotFound();
		}
	}
}