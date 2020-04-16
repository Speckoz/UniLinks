using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLink.API.Attributes;
using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly IStudentBusiness _studentBusiness;

		public StudentsController(IStudentBusiness studentBusiness) =>
			_studentBusiness = studentBusiness;

		// GET: /students/:courseName
		[HttpGet("{courseId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByCoordIdTaskAsync([Required]Guid courseId)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
				if (await _studentBusiness.FindAllByCourse(coordId, courseId) is IList<StudentModel> student)
					return Ok(student);
			}

			return BadRequest("O Coordenador nao tem acesso a esse curso!");
		}

		// POST: /students
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddStudentTaskAsync([FromBody]StudentModel student)
		{
			if (ModelState.IsValid)
			{
				StudentModel createdStudent = await _studentBusiness.AddTaskAsync(student);
				return Created("/students", createdStudent);
			}

			return BadRequest();
		}
	}
}