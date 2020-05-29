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
	public class ClassesController : ControllerBase
	{
		private readonly IClassBusiness _classBusiness;
		private readonly ICourseBusiness _courseBusiness;

		public ClassesController(IClassBusiness classBusiness, ICourseBusiness courseBusiness)
		{
			_classBusiness = classBusiness;
			_courseBusiness = courseBusiness;
		}

		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddClassTaskAsync([FromBody] ClassVO classVO)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != classVO.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar salas em outro curso!");

				if (await _classBusiness.FindByURITaskAsync(classVO.URI) is ClassVO)
					return Conflict("Ja existe uma sala com esse link");

				if (await _classBusiness.AddTasAsync(classVO) is ClassVO addedClass)
					return Created($"/Classes/{addedClass.ClassId}", addedClass);

				return BadRequest("Nao possivel adicionar a sala, verifique se os campos estao corretos.");
			}

			return BadRequest();
		}

		[HttpGet("{classId}")]
		[Authorizes]
		public async Task<IActionResult> GetClassTaskAsync([Required] Guid classId)
		{
			if (ModelState.IsValid)
			{
				if (await _classBusiness.FindByClassIdTaskAsync(classId) is ClassVO @class)
					return Ok(@class);

				return NotFound("A sala informada nao foi encontrada!");
			}

			return BadRequest();
		}

		[HttpGet("all")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> GetClassesTaskAsync()
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (!(await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course))
					return NotFound("Nao existe nenhum curso com o coordenador informado!");

				if (await _classBusiness.FindAllByCourseIdTaskAsync(course.CourseId) is List<ClassVO> classVO)
				{
					if (classVO.Count <= 0)
						return NotFound("Nao foi possivel encontrar salas com as informaçoes inseridas!");

					return Ok(classVO);
				}

				return NotFound("Nao foi possivel encontrar salas com as informaçoes inseridas!");
			}

			return BadRequest();
		}

		[HttpGet("all/{courseId}")]
		[Authorizes]
		public async Task<IActionResult> GetClassesTaskAsync([Required] Guid courseId, [Required] int period)
		{
			if (ModelState.IsValid)
			{
				if (await _classBusiness.FindAllByCourseIdAndPeriodTaskAsync(courseId, period) is List<ClassVO> classVO)
				{
					if (classVO.Count <= 0)
						return NotFound("Nao foi possivel encontrar salas com as informaçoes inseridas!");

					return Ok(classVO);
				}

				return NotFound("Nao foi possivel encontrar salas com as informaçoes inseridas!");
			}

			return BadRequest();
		}

		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UptadeClassTaskAsync([FromBody] ClassVO newClass)
		{
			if (ModelState.IsValid)
			{
				if (!(await _classBusiness.FindByClassIdTaskAsync(newClass.ClassId) is ClassVO classVO))
					return NotFound("Nao existe nenhuma sala com o Id informado");

				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != newClass.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar salas em outro curso!");

				if (await _classBusiness.FindByURITaskAsync(newClass.URI) is ClassVO currentCourse)
					if (currentCourse.ClassId != newClass.ClassId)
						return Conflict("Ja existe uma sala com este link");

				if (await _classBusiness.UpdateTaskAsync(newClass) is ClassVO updatedClass)
					return Ok(updatedClass);

				return BadRequest("Nao foi possivel atualizar as informaçoes, verifique se informou os valores corretamente!");
			}

			return BadRequest();
		}

		[HttpDelete("{classId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> RemoveClassTaskAsync([Required] Guid classId)
		{
			if (ModelState.IsValid)
			{
				if (!(await _classBusiness.FindByClassIdTaskAsync(classId) is ClassVO classVO))
					return NotFound("Nao foi possivel encontrar a sala informada!");

				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != classVO.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar salas em outro curso!");

				await _classBusiness.RemoveAsync(classVO.ClassId);
				return NoContent();
			}

			return BadRequest();
		}
	}
}