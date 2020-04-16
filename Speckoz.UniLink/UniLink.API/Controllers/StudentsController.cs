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

		// GET: /students/:courseId
		[HttpGet("{courseId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByCoordIdTaskAsync([Required]Guid courseId)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
				if (await _studentBusiness.FindAllByCoordIdAndCourseId(coordId, courseId) is IList<StudentModel> student)
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

		// DELETE: /students/:studentId
		[HttpDelete("{studentId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteStudentTaskAsync(int studentId)
		{
			if (ModelState.IsValid)
			{
				if (await _studentBusiness.FindByIdTaskAsync(studentId) is StudentModel student)
					if (student.Course.CoordinatorId == Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
					{
						await _studentBusiness.DeleteTaskAsync(student.Id);
						return NoContent();
					}
					else
						return Unauthorized("Voce nao é coordenador do curso do estudante para exclui-lo!");
				else
					return NotFound("O estudante informado nao existe");
			}

			return BadRequest();
		}
	}
}