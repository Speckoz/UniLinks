using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Enums;

namespace UniLinks.API.Controllers
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

		[HttpPost("add")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddTaskAsync([FromBody] DisciplineVO newDiscipline)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.ExistsByNameAndCourseIdTaskAsync(newDiscipline.Name, newDiscipline.CourseId))
					return Conflict("Ja existe uma disciplina com esse nome");

				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != newDiscipline.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar aulas em outro curso!");

				if (string.IsNullOrEmpty(newDiscipline.Name))
					return BadRequest("É necessario informar o nome da disciplina!");

				if (string.IsNullOrEmpty(newDiscipline.Teacher))
					return BadRequest("É necessario informar o nome do professor!");

				if (newDiscipline.Period <= 0)
					return BadRequest("O periodo precisa ser maior que zero!");

				if (newDiscipline.ClassId == Guid.Empty)
					return BadRequest("É necessario informar a sala!");

				if (await _disciplineBusiness.AddTaskAsync(newDiscipline) is DisciplineVO addedDiscipline)
					return Created("/disciplines", addedDiscipline);

				return BadRequest("Algo deu errado, verifique os valores e tente novamente");
			}

			return BadRequest();
		}

		[HttpPost("all")]
		[Authorizes]
		public async Task<IActionResult> GetDisciplinesTaskAsync([FromBody] List<Guid> disciplines)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.FindAllByDisciplineIdsTaskAsync(disciplines) is List<DisciplineVO> discs)
					return Ok(discs);

				return NotFound("Uma ou todas as disciplinas requisitadas nao foram localizadas!");
			}

			return BadRequest();
		}

		[HttpGet("{disciplineId}")]
		[Authorizes]
		public async Task<IActionResult> GetDisciplineByDisciplineId([Required] Guid disciplineId)
		{
			if (ModelState.IsValid)
			{
				if (await _disciplineBusiness.FindByDisciplineIdTaskAsync(disciplineId) is DisciplineVO discipline)
					return Ok(discipline);

				return NotFound("Nao existe nenhuma disciplina com o Id informado");
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

				return NotFound("O curso nao possivel nenhuma disciplina!");
			}

			return BadRequest();
		}

		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody] DisciplineVO newDiscipline)
		{
			if (ModelState.IsValid)
			{
				if (!(await _disciplineBusiness.FindByDisciplineIdTaskAsync(newDiscipline.DisciplineId) is DisciplineVO currentDiscipline))
					return NotFound("Nao existe uma disciplina com esse Id");

				CourseVO course = await _courseBusiness.FindByCourseIdTaskAsync(currentDiscipline.CourseId);

				if (course.CoordinatorId != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
					return Unauthorized("Voce nao tem autorizaçao para alterar uma disciplina de outro curso!");

				newDiscipline.CourseId = course.CourseId;

				if (await _disciplineBusiness.ExistsByNameAndCourseIdTaskAsync(newDiscipline.Name, newDiscipline.CourseId))
					if (newDiscipline.Name != currentDiscipline.Name)
						return Conflict("Ja existe uma disciplina com esse nome");

				if (string.IsNullOrEmpty(newDiscipline.Name))
					return BadRequest("É necessario informar o nome da disciplina!");

				if (string.IsNullOrEmpty(newDiscipline.Teacher))
					return BadRequest("É necessario informar o nome do professor!");

				if (newDiscipline.Period <= 0)
					return BadRequest("O periodo precisa ser maior que zero!");

				if (newDiscipline.ClassId == Guid.Empty)
					return BadRequest("É necessario informar a sala!");

				if (await _disciplineBusiness.UpdateTaskAync(newDiscipline) is DisciplineVO disciplineUpdated)
					return Ok(disciplineUpdated);
			}

			return BadRequest();
		}

		[HttpDelete("{disciplineId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteTaskAsync([Required] Guid disciplineId)
		{
			if (ModelState.IsValid)
			{
				if (!(await _disciplineBusiness.FindByDisciplineIdTaskAsync(disciplineId) is DisciplineVO discipline))
					return NotFound("Nao existe uma disciplina com esse Id");

				CourseVO course = await _courseBusiness.FindByCourseIdTaskAsync(discipline.CourseId);

				if (course.CoordinatorId != Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
					return Unauthorized("Voce nao tem autorizaçao para remover uma disciplina de outro curso!");

				await _disciplineBusiness.DeleteTaskAsync(discipline.DisciplineId);
				return NoContent();
			}

			return BadRequest();
		}
	}
}