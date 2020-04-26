using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Enums;

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

		// POST: /students
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddStudentTaskAsync([FromBody]StudentVO student)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					student.CourseId = course.CourseId;

				//if (!await _courseBusiness.ExistsCoordInCourseTaskAsync(coordId, student.CourseId))
				//	return BadRequest("Nao é possivel adicionar um estudante em um curso que nao é coordenador.");

				if (await _studentBusiness.ExistsByEmailTaskAsync(student.Email))
					return Conflict("Ja existe um aluno com esse email!");

				if (await _studentBusiness.AddTaskAsync(student) is StudentDisciplineVO createdStudent)
					return Created("/students", createdStudent);

				return BadRequest("O formato das disciplinas do estudante nao está valida (guid;guid;guid)");
			}

			return BadRequest();
		}

		// GET: /students/:courseId
		[HttpGet("{courseId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByCoordIdTaskAsync([Required]Guid courseId)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _studentBusiness.FindAllByCoordIdAndCourseId(coordId, courseId) is IList<StudentDisciplineVO> studentDiscpline)
					return Ok(studentDiscpline);

				return BadRequest("Nao foi encontrado nenhum aluno no curso informado ou algum aluno nao possui as disciplinas no formato correto.");
			}

			return BadRequest("O Coordenador nao tem acesso a esse curso!");
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
					CourseVO course = await _courseBusiness.FindByCoordIdTaskAsync(coordId);

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