using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Data.VO.Lesson;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LessonsController : ControllerBase
	{
		private readonly ILessonBusiness _lessonBusiness;
		private readonly ICourseBusiness _courseBusiness;

		public LessonsController(ILessonBusiness lessonBusiness, ICourseBusiness courseBusiness)
		{
			_lessonBusiness = lessonBusiness;
			_courseBusiness = courseBusiness;
		}

		// POST: /Lessons
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddClassTaskAsync([FromBody] LessonVO lesson)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != lesson.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar aulas em outro curso!");

				if (await _lessonBusiness.FindByURITaskAsync(lesson.URI) is LessonVO)
					return Conflict("A aula informada ja existe, verifique se o link está correto");

				if (await _lessonBusiness.AddTaskAsync(lesson) is LessonVO addedClass)
					return Created($"/lessons/{addedClass.LessonId}", addedClass);
			}

			return BadRequest();
		}

		// GET: /Lessons/:id
		[HttpGet("{id}")]
		[Authorize]
		public async Task<IActionResult> FindByIdTaskAsync(Guid id)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.FindByIdTaskAsync(id) is LessonVO lesson)
					return Ok(lesson);

				return NotFound("A aula informada nao existe!");
			}

			return BadRequest();
		}

		[HttpGet("all/{disciplines}")]
		[Authorizes(UserTypeEnum.Student, UserTypeEnum.Coordinator)]
		public async Task<IActionResult> FindAllByDisciplines([Required] string disciplines)
		{
			if (ModelState.IsValid)
			{
				if ((await _lessonBusiness.FindAllByDisciplinesIdTaskASync(disciplines)) is IList<LessonDisciplineVO> lessons)
					return Ok(lessons);

				return NotFound("Nao foi possivel encontrar as aulas requisitadas.");
			}

			return BadRequest();
		}

		// PUT: /Lessons
		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody] LessonVO newLesson)
		{
			if (ModelState.IsValid)
			{
				var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

				if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
					if (course.CourseId != newLesson.CourseId)
						return Unauthorized("Voce nao tem permissao para adicionar aulas em outro curso!");

				if (await _lessonBusiness.UpdateTaskAsync(newLesson) is LessonVO lesson)
					return Ok(lesson);

				return NotFound("Nao foi possivel atualizar os dados, verifique se a aula realmente existe!");
			}

			return BadRequest();
		}

		// DELETE: /Lessons/:id
		[HttpDelete("{lessonId}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteTaskAsync([Required] Guid lessonId)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.FindByIdTaskAsync(lessonId) is LessonVO lesson)
				{
					var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

					if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
						if (coordId != course.CoordinatorId)
							return Unauthorized("Voce nao tem permissao para remover aulas em outro curso!");

					await _lessonBusiness.DeleteAsync(lessonId);
					return NoContent();
				}

				return BadRequest("Aula informada nao existe");
			}

			return BadRequest();
		}
	}
}