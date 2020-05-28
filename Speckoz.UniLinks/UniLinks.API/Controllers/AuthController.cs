using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.API.Models.Auxiliary;
using UniLinks.Dependencies.Data.VO.Coordinator;
using UniLinks.Dependencies.Data.VO.Student;
using UniLinks.Dependencies.Models.Auxiliary;

namespace UniLinks.API.Controllers
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
        [HttpPost("coordinator")]
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
        [HttpPost("student")]
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