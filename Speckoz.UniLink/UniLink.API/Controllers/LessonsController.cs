using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UniLink.API.Attributes;
using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

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
		public async Task<IActionResult> AddClassTaskAsync([FromBody]LessonModel lesson)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.AddTaskAsync(lesson) is LessonModel addedClass)
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
				if (await _lessonBusiness.FindByIdTaskAsync(id) is LessonModel lesson)
					return Ok(lesson);

				return NotFound("A aula informada nao existe!");
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
				if (await _lessonBusiness.FindByURITaskAsync(uri) is LessonModel lesson)
					return Ok(lesson);

				return NotFound("A aula informada nao existe!");
			}

			return BadRequest();
		}

		// POST: /Lessons/date
		[HttpPost("date")]
		[Authorize]
		public async Task<IActionResult> FindByDateTaskAsync([FromBody]DateTime date)
		{
			throw new NotImplementedException();
		}

		// POST: /Lessons/course
		[HttpPost("course")]
		[Consumes("text/plain")]
		[Authorize]
		public async Task<IActionResult> FindByCourseTaskAsync([FromBody]string course)
		{
			throw new NotImplementedException();
		}

		// PUT: /Lessons
		[HttpPut]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> UpdateTaskAsync([FromBody]LessonModel newLesson)
		{
			if (ModelState.IsValid)
			{
				if (await _lessonBusiness.UpdateTaskAsync(newLesson) is LessonModel lesson)
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