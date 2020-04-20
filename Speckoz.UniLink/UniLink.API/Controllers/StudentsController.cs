using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentsController : ControllerBase
	{
		private readonly IStudentBusiness _studentBusiness;
		private readonly ICourseBusiness _courseBusiness;

		public StudentsController(IStudentBusiness studentBusiness, ICourseBusiness courseBusiness)
		{
			_studentBusiness = studentBusiness;
			_courseBusiness = courseBusiness;
		}

		// GET: /students/:courseId
		[HttpGet("{courseId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByCoordIdTaskAsync([Required]Guid courseId)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _studentBusiness.FindAllByCoordIdAndCourseId(coordId, courseId) is IList<StudentVO> student)
					return Ok(student);
			}

			return BadRequest("O Coordenador nao tem acesso a esse curso!");
		}

		// POST: /students
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddStudentTaskAsync([FromBody]StudentVO student)
		{
			if (ModelState.IsValid)
			{
				StudentVO createdStudent = await _studentBusiness.AddTaskAsync(student);
				return Created("/students", createdStudent);
			}

			return BadRequest();
		}

		// DELETE: /students/:studentId
		[HttpDelete("{studentId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteStudentTaskAsync(Guid studentId)
		{
			if (ModelState.IsValid)
			{
				if (await _studentBusiness.FindByIdTaskAsync(studentId) is StudentVO student) 
				{
					var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
					var course = await _courseBusiness.FindByCoordIdTaskAsync(studentId);

					if (course.CoordinatorId == coordId)
					{
						await _studentBusiness.DeleteTaskAsync(student.StudentId);
						return NoContent();
					}
					else
						return Unauthorized("Voce nao é coordenador do curso do estudante para exclui-lo!");
				}
				else
					return NotFound("O estudante informado nao existe");
			}

			return BadRequest();
		}
	}
}