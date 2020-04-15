using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Models.Auxiliary;
using UniLink.Dependencies.Models;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAccountBusiness _accountBusiness;

		public AuthController(IAccountBusiness accountBusiness) => _accountBusiness = accountBusiness;

		[HttpPost]
		public async Task<IActionResult> AuthAccountTaskAsync([FromBody]LoginRequestModel userLogin)
		{
			if (ModelState.IsValid)
			{
				if (await _accountBusiness.AuthAccountTaskAsync(userLogin) is UserModel user)
					return Ok(user);

				return BadRequest("As credenciais informadas estao incorretas!");
			}

			return BadRequest();
		}

		[HttpPost("User")]
		public async Task<IActionResult> AuthUserTaskAsync([FromBody]EmailFromBody email)
		{
			if (ModelState.IsValid)
			{
				if (await _accountBusiness.AuthUserTaskAsync(email.Email) is UserModel user)
					return Ok(user);

				return BadRequest("Nao foi possivel encontrar um aluno com este email!");
			}

			return BadRequest();
		}
	}
}