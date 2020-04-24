using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LessonsController : ControllerBase
	{
		private readonly ILessonBusiness _lessonBusiness;

		public LessonsController(ILessonBusiness lessonBusiness) =>
			_lessonBusiness = lessonBusiness;

		// POST: /Lessons
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddClassTaskAsync([FromBody]LessonVO lesson)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.AddTaskAsync(lesson) is LessonVO addedClass)
					return Created($"/classes/newLesson{addedClass.LessonId}", addedClass);

				return BadRequest("A aula informada ja existe, informe uma URI diferente");
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
		[Authorizes(UserTypeEnum.Student)]
		public async Task<IActionResult> FindAllByDisciplines([Required]string disciplines)
		{
			if (ModelState.IsValid)
			{
				if ((await _lessonBusiness.FindAllByDisciplinesIdTaskASync(disciplines)) is IList<LessonVO> lessons)
					return Ok(lessons);

				return NotFound("Nao foi possivel encontrar as aulas requisitadas.");
			}

			return BadRequest();
		}

		// POST: /Lessons/uri
		[HttpPost("uri")]
		[Consumes("text/plain")]
		[Authorize]
		public async Task<IActionResult> FindByURITaskAsync([FromBody]string uri)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.FindByURITaskAsync(uri) is LessonVO lesson)
					return Ok(lesson);

				return NotFound("A aula informada nao existe!");
			}

			return BadRequest();
		}

		// PUT: /Lessons
		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody]LessonVO newLesson)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.UpdateTaskAsync(newLesson) is LessonVO lesson)
					return Ok(lesson);

				return NotFound("Nao foi possivel atualizar os dados, verifique se a aula realmente existe!");
			}

			return BadRequest();
		}

		// DELETE: /Lessons/:id
		[HttpDelete("{id}")]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> DeleteTaskAsync([Required]Guid id)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.DeleteTaskAsync(id))
					return NoContent();

				return BadRequest("Turma informada nao existe");
			}

			return BadRequest();
		}
	}
}