using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Models;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserBusiness _userBusiness;

		public UserController(IUserBusiness userBusiness) =>
			_userBusiness = userBusiness;

		[HttpPost]
		public async Task<IActionResult> AuthUserTaskAsync([FromBody]EmailFromBody email)
		{
			if (ModelState.IsValid)
			{
				if (await _userBusiness.AuthUserTaskAsync(email.Email) is UserModel user)
					return Ok(user);

				return BadRequest("Nao foi possivel encontrar um aluno com este email!");
			}

			return BadRequest();
		}
	}

	// auxiliay model
	public class EmailFromBody
	{
		[Required]
		public string Email { get; set; }
	}
}