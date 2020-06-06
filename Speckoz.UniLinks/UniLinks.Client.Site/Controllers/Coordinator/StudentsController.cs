using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

using UniLinks.Client.Site.Services.Coordinator;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Enums;
using UniLinks.Dependencies.Models;

namespace UniLinks.Client.Site.Controllers.Coordinator
{
	[Route("Coordinator/[Controller]")]
	[Authorizes(UserTypeEnum.Coordinator)]
	public class StudentsController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> Index([FromServices] StudentsService studentsService)
		{
			string token = User.FindFirst("Token").Value;

			ResultModel<List<StudentVO>> students = await studentsService.GetStudentsTaskAsync(token);
			return View("/Views/Coordinator/Students/Index.cshtml", students);
		}

		[HttpGet("Update/{studentId}")]
		public async Task<IActionResult> Update([FromServices] StudentsService studentsService, [Required] Guid studentId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<StudentVO> response = await studentsService.GetStudentTaskAsync(token, studentId);

				return View("/Views/Coordinator/Students/Update.cshtml", response);
			}

			return BadRequest();
		}

		[HttpPost("Update")]
		public async Task<IActionResult> Update([FromServices] StudentsService studentsService, ResultModel<StudentVO> request)
		{
			string token = User.FindFirst("Token").Value;
			var courseId = Guid.Parse(User.FindFirst("CourseId").Value);

			if (ModelState.IsValid)
			{
				ResultModel<StudentVO> response = await studentsService.UpdateStudentTaskAsync(request.Object, token, courseId);

				if (response.StatusCode != HttpStatusCode.OK)
					return View("/Views/Coordinator/Students/Update.cshtml", new ResultModel<StudentVO>
					{
						Object = request.Object,
						Message = response.Message,
						StatusCode = response.StatusCode
					});

				ResultModel<List<StudentVO>> studentResponse = await studentsService.GetStudentsTaskAsync(token);

				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;

				return response.StatusCode switch
				{
					HttpStatusCode.OK => View("/Views/Coordinator/Students/Index.cshtml", studentResponse),
					_ => View("/Views/Coordinator/Students/Update.cshtml", studentResponse)
				};
			}

			return NotFound();
		}

		[HttpPost("Delete/{studentId}")]
		public async Task<IActionResult> Delete([FromServices] StudentsService studentsService, [Required] Guid studentId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<bool> response = await studentsService.RemoveStudentTaskAsync(studentId, token);
				ResultModel<List<StudentVO>> studentResponse = await studentsService.GetStudentsTaskAsync(token);

				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;

				return View("/Views/Coordinator/Students/Index.cshtml", studentResponse);
			}

			return NotFound();
		}
	}
}