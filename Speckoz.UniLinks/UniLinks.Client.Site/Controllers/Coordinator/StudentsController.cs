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

			ResultModel<List<StudentDisciplineVO>> students = await studentsService.GetStudentsTaskAsync(token);
			return View("/Views/Coordinator/Students/Index.cshtml", students);
		}

		[HttpGet("Add")]
		public IActionResult Add() => View("/Views/Coordinator/Students/Add.cshtml");

		[HttpPost("Add")]
		public async Task<IActionResult> AddStudent([FromServices] StudentsService studentsService, ResultModel<StudentDisciplineVO> request)
		{
			string token = User.FindFirst("Token").Value;

			request.Object.Student.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<StudentDisciplineVO> response = await studentsService.AddStudentTaskAsync(request.Object.Student, token);

			if (response.StatusCode != HttpStatusCode.Created)
				return View("/Views/Coordinator/Students/Add.cshtml", new ResultModel<StudentDisciplineVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<StudentDisciplineVO>> studentResponse = await studentsService.GetStudentsTaskAsync(token);

			if (studentResponse.StatusCode == HttpStatusCode.OK)
			{
				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;
			}

			return studentResponse.StatusCode switch
			{
				HttpStatusCode.Created => View("/Views/Coordinator/Students/Index.cshtml", studentResponse),
				_ => View("/Views/Coordinator/Students/Add.cshtml", studentResponse)
			};
		}

		[HttpGet("Update/{studentId}")]
		public async Task<IActionResult> Update([FromServices] StudentsService studentsService, [Required] Guid studentId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<StudentDisciplineVO> response = await studentsService.GetStudentTaskAsync(token, studentId);

				return View("/Views/Coordinator/Students/Update.cshtml", response);
			}

			return BadRequest();
		}

		[HttpPost("Update")]
		public async Task<IActionResult> UpdateStudent([FromServices] StudentsService studentsService, ResultModel<StudentDisciplineVO> request)
		{
			string token = User.FindFirst("Token").Value;

			request.Object.Student.CourseId = Guid.Parse(User.FindFirst("CourseId").Value);

			ResultModel<StudentDisciplineVO> response = await studentsService.UpdateStudentTaskAsync(request.Object.Student, token);

			if (response.StatusCode != HttpStatusCode.OK)
				return View("/Views/Coordinator/Students/Update.cshtml", new ResultModel<StudentDisciplineVO>
				{
					Object = request.Object,
					Message = response.Message,
					StatusCode = response.StatusCode
				});

			ResultModel<List<StudentDisciplineVO>> studentResponse = await studentsService.GetStudentsTaskAsync(token);

			if (studentResponse.StatusCode == HttpStatusCode.OK)
			{
				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;
			}

			return studentResponse.StatusCode switch
			{
				HttpStatusCode.OK => View("/Views/Coordinator/Students/Index.cshtml", studentResponse),
				_ => View("/Views/Coordinator/Students/Update.cshtml", studentResponse)
			};
		}

		[HttpPost("Delete/{studentId}")]
		public async Task<IActionResult> Delete([FromServices] StudentsService studentsService, [Required] Guid studentId)
		{
			if (ModelState.IsValid)
			{
				string token = User.FindFirst("Token").Value;

				ResultModel<bool> response = await studentsService.RemoveStudentTaskAsync(studentId, token);
				ResultModel<List<StudentDisciplineVO>> studentResponse = await studentsService.GetStudentsTaskAsync(token);

				studentResponse.Message = response.Message;
				studentResponse.StatusCode = response.StatusCode;

				return View("/Views/Coordinator/Students/Index.cshtml", studentResponse);
			}

			return NotFound();
		}
	}
}