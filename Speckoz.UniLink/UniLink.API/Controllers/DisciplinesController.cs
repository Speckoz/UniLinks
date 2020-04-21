using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DisciplinesController : ControllerBase
	{
		private readonly IDisciplineBusiness _disciplineBusiness;

		public DisciplinesController(IDisciplineBusiness disciplineBusiness)
		{
			_disciplineBusiness = disciplineBusiness;
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
	}
}