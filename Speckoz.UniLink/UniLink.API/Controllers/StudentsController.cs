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
		public async Task<IActionResult> AddStudentTaskAsync([FromBody] StudentVO student)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != student.CourseId)
						return Unauthorized("Voce nao tem permissao para atualizar informaçoes de um aluno de outro curso!");

				if (await _studentBusiness.ExistsByEmailTaskAsync(student.Email))
					return Conflict("Ja existe um aluno com esse email!");

				if (await _studentBusiness.AddTaskAsync(student) is StudentDisciplineVO createdStudent)
					return Created("/students", createdStudent);

				return BadRequest("O formato das disciplinas do estudante nao está valida (guid;guid;guid)");
			}

			return BadRequest();
		}

		// GET: /students/all
		[HttpGet("all")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByCoordIdTaskAsync()
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course))
					return NotFound("Nao existe nenhum curso referente ao coordenador!");

				if (await _studentBusiness.FindAllByCourseIdTaskAsync(course.CourseId) is List<StudentDisciplineVO> studentDiscpline)
					return Ok(studentDiscpline);

				return BadRequest("Nao foi encontrado nenhum aluno no curso informado ou algum aluno nao possui as disciplinas no formato correto.");
			}

			return BadRequest("O Coordenador nao tem acesso a esse curso!");
		}

		// PUT: /students/:studentId
		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateStudentTaskAsync([FromBody] StudentVO newStudent)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != newStudent.CourseId)
						return Unauthorized("Voce nao tem permissao para atualizar informaçoes de um aluno de outro curso!");

				if (!(await _studentBusiness.FindByIdTaskAsync(newStudent.StudentId) is StudentVO studentVO))
					return NotFound("Nao existe um aluno com esse Id");

				if (await _studentBusiness.UpdateTaskAsync(studentVO, newStudent) is StudentVO student)
					return Ok(student);

				return UnprocessableEntity("Nao foi possivel atualizar os dados, verifique se o estudante realmente existe!");
			}

			return BadRequest("Todos os campos são obrigatórios");
		}

		// DELETE: /students/:studentId
		[HttpDelete("{studentId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteStudentTaskAsync([Required] Guid studentId)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _studentBusiness.FindByIdTaskAsync(studentId) is StudentVO studentVO))
					return NotFound("Nao existe um aluno com esse Id");

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != studentVO.CourseId)
						return Unauthorized("Voce nao tem permissao para deletar um aluno de outro curso!");

				await _studentBusiness.DeleteTaskAsync(studentVO.StudentId);
				return NoContent();
			}

			return BadRequest();
		}
	}
}