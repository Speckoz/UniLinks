using Microsoft.AspNetCore.Mvc;

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UniLink.Dependencies.Attributes;
using UniLink.Dependencies.Data.VO;
using UniLink.Dependencies.Enums;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassesController : ControllerBase
	{
		[HttpPost]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> AddClassTaskAsync([FromBody] ClassVO classVO)
		{
			return BadRequest();
		}

		[HttpGet("{classId}")]
		[Authorizes]
		public async Task<IActionResult> GetClassTaskAsync([Required] Guid classId)
		{
			return BadRequest();
		}

		[HttpDelete]
		[Authorizes(UserTypeEnum.Coordinator)]
		public async Task<IActionResult> RemoveClassTaskAsync([Required] Guid classId)
		{
			return BadRequest();
		}
	}
}