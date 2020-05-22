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

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DisciplinesController : ControllerBase
	{
		private readonly IDisciplineBusiness _disciplineBusiness;
		private readonly ICourseBusiness _courseBusiness;

		public DisciplinesController(IDisciplineBusiness disciplineBusiness, ICourseBusiness courseBusiness)
		{
			_disciplineBusiness = disciplineBusiness;
			_courseBusiness = courseBusiness;
		}

		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddTaskAsync([FromBody] DisciplineVO discipline)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.ExistsByNameAndCourseIdTaskAsync(discipline.Name, discipline.CourseId))
					return Conflict("Ja existe uma disciplina com esse nome");

				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != discipline.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar aulas em outro curso!");

				if (await _disciplineBusiness.AddTaskAsync(discipline) is DisciplineVO addedDiscipline)
					return Created("/disciplines", addedDiscipline);

				return BadRequest("Algo deu errado, verifique os valores e tente novamente");
			}

			return BadRequest();
		}

		[HttpGet("{disciplines}")]
		[Authorizes]
		public async Task<IActionResult> GetDisciplinesTaskAsync([Required] string disciplines)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.FindDisciplinesTaskAsync(disciplines) is List<DisciplineVO> discs)
					return Ok(discs);

				return NotFound("Nenhuma disciplina foi encontrada com a entrada fornecida, verifique se formato está correto (guid;guid;guid)");
			}

			return BadRequest();
		}

		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> GetDisciplinesByCoordIdTaskAsync()
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
				{
					if (await _disciplineBusiness.FindByCourseIdTaskAsync(course.CourseId) is List<DisciplineVO> discs)
						return Ok(discs);
				}

				return NotFound("Nenhuma disciplina foi encontrada com a entrada fornecida, verifique se formato está correto (guid;guid;guid)");
			}

			return BadRequest();
		}

		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody] DisciplineVO newDiscipline)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.FindByDisciplineIdTaskAsync(newDiscipline.DisciplineId) is DisciplineVO discipline)
				{
					CourseVO course = await _courseBusiness.FindByCourseIdTaskAsync(discipline.CourseId);

					if (course.CoordinatorId != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
						return Unauthorized("Voce nao tem autorizaçao para alterar uma disciplina de outro curso!");

					newDiscipline.CourseId = course.CourseId;

					if (!(await _disciplineBusiness.FindByDisciplineIdTaskAsync(newDiscipline.DisciplineId) is DisciplineVO))
						return NotFound("Nao existe uma disciplina com esse Id");

					if (await _disciplineBusiness.ExistsByNameAndCourseIdTaskAsync(newDiscipline.Name, newDiscipline.CourseId) && !discipline.Name.Equals(newDiscipline.Name))
						return Conflict("Ja existe uma disciplina com esse nome");

					if (await _disciplineBusiness.UpdateTaskAync(newDiscipline) is DisciplineVO disciplineUpdated)
						return Ok(disciplineUpdated);
				}
				else
					return NotFound("Nao existe uma disciplina com esse Id");
			}

			return BadRequest();
		}

		[HttpDelete("{disciplineId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteTaskAsync([Required] Guid disciplineId)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.FindByDisciplineIdTaskAsync(disciplineId) is DisciplineVO discipline)
				{
					CourseVO course = await _courseBusiness.FindByCourseIdTaskAsync(discipline.CourseId);

					if (course.CoordinatorId != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
						return Unauthorized("Voce nao tem autorizaçao para remover uma disciplina de outro curso!");

					await _disciplineBusiness.DeleteTaskAsync(discipline.DisciplineId);
					return NoContent();
				}
				else
					return NotFound("Nao existe uma disciplina com esse Id");
			}

			return BadRequest();
		}
	}
}