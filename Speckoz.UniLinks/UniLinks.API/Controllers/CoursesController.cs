﻿using Microsoft.AspNetCore.Mvc;

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
	[Authorizes(UserTypeEnum.Coordinator)]
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
		public async Task<IActionResult> AddClassTaskAsync([FromBody] CourseVO newCourse)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _coordinatorBusiness.FindByCoordIdTaskAsync(coordId) is AuthCoordinatorVO coordinator))
					return NotFound("Não existe nenhum coordenador com o Id informado");

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO)
					return Conflict("Ja existe um curso com o coordenador informado!");

				if (string.IsNullOrEmpty(newCourse.Name))
					return BadRequest("É necessario informar o nome do curso!");

				if (newCourse.Periods <= 0)
					return BadRequest("A quantidade de periodos precisa ser maior que zero");

				if (await _courseBusiness.ExistsWithNameTaskAsync(newCourse.Name))
					return Conflict("Ja existe um curso com esse nome");

				newCourse.CoordinatorId = coordinator.CoordinatorId;
				newCourse.CourseId = coordinator.CourseId;

				if (await _courseBusiness.AddTaskAsync(newCourse) is CourseVO addedCourse)
					return Created("/courses", addedCourse);

				return BadRequest("Algo deu errado, Tente novamente!");
			}

			return BadRequest();
		}

		[HttpGet]
		public async Task<IActionResult> CourseByCoordIdTaskAsync()
		{
			var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

			if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
				return Ok(course);

			return NotFound("Não existe nenhum curso com este coordenador");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateTaskAsync([FromBody] CourseVO newCourse)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _courseBusiness.FindByCourseIdTaskAsync(newCourse.CourseId) is CourseVO currentCourse))
					return NotFound("Não existe nenhum curso com esse Id");

				if (currentCourse.CoordinatorId != coordId)
					return Unauthorized("Você não tem permissão para alterar informações do curso onde não é coordenador!");

				if (string.IsNullOrEmpty(newCourse.Name))
					return BadRequest("É necessario informar o nome do curso!");

				if (newCourse.Periods <= 0)
					return BadRequest("A quantidade de periodos precisa ser maior que zero");

				if (await _courseBusiness.ExistsWithNameTaskAsync(newCourse.Name))
					if (newCourse.Name != currentCourse.Name)
						return Conflict("Ja existe um curso com esse nome!");

				if (await _courseBusiness.UpdateTaskAsync(newCourse) is CourseVO updatedCourse)
					return Created("/Courses", updatedCourse);
			}

			return BadRequest();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteTaskAsync()
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO currentCourse))
					return NotFound("Não existe nenhum curso com o coordenador informado!");

				await _courseBusiness.DeleteAsync(currentCourse.CourseId);
				return NoContent();
			}

			return BadRequest();
		}
	}
}