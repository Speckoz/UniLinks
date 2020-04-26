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

		[HttpGet("{disciplines}")]
		[Authorizes]
		public async Task<IActionResult> GetDisciplinesTaskAsync([Required]string disciplines)
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
	}
}