using Microsoft.AspNetCore.Mvc;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Enums;

namespace UniLinks.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseBusiness _courseBusiness;
		private readonly ICoordinatorBusiness _coordinatorBusiness;

		public CoursesController(ICourseBusiness courseBusiness, ICoordinatorBusiness coordinatorBusiness)
		{
			_courseBusiness = courseBusiness;
			_coordinatorBusiness = coordinatorBusiness;
		}

		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddClassTaskAsync([FromBody] CourseVO newCourse)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _coordinatorBusiness.FindByCoordIdTaskAsync(coordId) is AuthCoordinatorVO coordinator))
					return NotFound("Nao existe nenhum coordenador com o Id informado");

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO)
					return Conflict("Ja existe um curso com o coordenador informado!");

				if (await _courseBusiness.ExistsWithNameTaskAsync(newCourse.Name))
					return Conflict("Ja existe um curso com esse nome");

				if (string.IsNullOrEmpty(newCourse.Name))
					return BadRequest("É necessario informar o nome do curso!");

				if (newCourse.Periods <= 0)
					return BadRequest("A quantidade de periodos precisa ser maior que zero");

				newCourse.CoordinatorId = coordinator.CoordinatorId;
				newCourse.CourseId = coordinator.CourseId;

				if (await _courseBusiness.AddTaskAsync(newCourse) is CourseVO addedCourse)
					return Created("/courses", addedCourse);

				return BadRequest("Algo deu errado, Tente novamente!");
			}

			return BadRequest();
		}

		[HttpGet]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> CourseByCoordIdTaskAsync()
		{
			var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
				return Ok(course);

			return NotFound("Nao existe nenhum curso com este coordenador");
		}

		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody] CourseVO newCourse)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _courseBusiness.FindByCourseIdTaskAsync(newCourse.CourseId) is CourseVO currentCourse))
					return NotFound("Nao existe nenhum curso com esse Id");

				if (currentCourse.CoordinatorId != coordId)
					return Unauthorized("Voce nao tem permissao para alterar informaçoes do curso onde nao é coordenador!");

				if (await _courseBusiness.ExistsWithNameTaskAsync(newCourse.Name))
					if (newCourse.Name != currentCourse.Name)
						return Conflict("Ja existe um curso com esse nome!");

				if (string.IsNullOrEmpty(newCourse.Name))
					return BadRequest("É necessario informar o nome do curso!");

				if (newCourse.Periods <= 0)
					return BadRequest("A quantidade de periodos precisa ser maior que zero");

				newCourse.CoordinatorId = coordId;

				if (await _courseBusiness.UpdateTaskAsync(currentCourse, newCourse) is CourseVO updatedCourse)
					return Ok(updatedCourse);
			}

			return BadRequest();
		}
	}
}