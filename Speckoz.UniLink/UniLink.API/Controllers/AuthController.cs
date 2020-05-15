using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UniLink.API.Business.Interfaces;
using UniLink.API.Models.Auxiliary;
using UniLink.Dependencies.Data.VO.Coordinator;
using UniLink.Dependencies.Data.VO.Student;
using UniLink.Dependencies.Models.Auxiliary;

namespace UniLink.API.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICoordinatorBusiness _coordinatorBusiness;
        private readonly IStudentBusiness _studentBusiness;

        public AuthController(ICoordinatorBusiness accountBusiness, IStudentBusiness studentBusiness)
        {
            _coordinatorBusiness = accountBusiness;
            _studentBusiness = studentBusiness;
        }

        // POST: /Auth
        [HttpPost]
        public async Task<IActionResult> AuthAccountTaskAsync([FromBody]LoginRequestModel userLogin)
        {
            if (ModelState.IsValid)
            {
                if (await _coordinatorBusiness.AuthAccountTaskAsync(userLogin) is AuthCoordinatorVO user)
                    return Ok(user);

                return BadRequest("As credenciais informadas estao incorretas!");
            }

            return BadRequest();
        }

        // POST: /Auth/User
        [HttpPost("User")]
        public async Task<IActionResult> AuthUserTaskAsync([FromBody]EmailFromBody email)
        {
            if (ModelState.IsValid)
            {
                if (await _studentBusiness.AuthUserTaskAsync(email.Email) is StudentVO user)
                    return Ok(user);

                return BadRequest("Nao foi possivel encontrar um aluno com este email!");
            }

            return BadRequest();
        }
    }
}