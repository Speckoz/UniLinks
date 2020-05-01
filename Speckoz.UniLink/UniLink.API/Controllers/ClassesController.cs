using Microsoft.AspNetCore.Mvc;

using System;
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
    public class ClassesController : ControllerBase
    {
        private readonly IClassBusiness _classBusiness;

        public ClassesController(IClassBusiness classBusiness)
        {
            _classBusiness = classBusiness;
        }

        [HttpPost]
        [Authorizes(UserTypeEnum.Coordinator)]
        public async Task<IActionResult> AddClassTaskAsync([FromBody] ClassVO classVO)
        {
            if (ModelState.IsValid)
            {
                if (await _classBusiness.FindByURITaskAsync(classVO.URI) is ClassVO)
                    return Conflict("Ja existe uma sala com esse link");

                if (await _classBusiness.AddTasAsync(classVO) is ClassVO addedClass)
                    return Created("/Classes", addedClass);

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

        [HttpDelete]
        [Authorizes(UserTypeEnum.Coordinator)]
        public async Task<IActionResult> RemoveClassTaskAsync([Required] Guid classId)
        {
            if (ModelState.IsValid)
            {
                if (await _classBusiness.FindByClassIdTaskAsync(classId) is ClassVO classVO)
                {
                    await _classBusiness.RemoveAsync(classVO);
                    return NoContent();
                }

                return NotFound("Nao foi possivel encontrra a aula informada!");
            }

            return BadRequest();
        }
    }
}