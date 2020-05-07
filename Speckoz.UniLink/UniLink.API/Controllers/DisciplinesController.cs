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
				if (await _disciplineBusiness.ExistsByNameTaskAsync(discipline.Name))
					return Conflict("Ja existe uma disciplina com esse nome");

				//adiciona o id do coordenador que fez a requisiçao.

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
				if (await _disciplineBusiness.FindDisciplinesTaskAsync(disciplines) is IList<DisciplineVO> discs)
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
					if (await _disciplineBusiness.FindByCourseIdTaskAsync(course.CourseId) is IList<DisciplineVO> discs)
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
				//verifica se nao existe pelo id
				if (!await _disciplineBusiness.ExistsByDisciplineIdTaskAsync(newDiscipline.DisciplineId))
					return Conflict("Nao existe uma disciplina com esse Id");

				//verifica se existe um disciplina no curso com o mesmo nome.

				//verifica se é do curso cujo o coordenador é do requisitante.

				//atualiza os dados
			}

			return BadRequest();
		}
	}
}