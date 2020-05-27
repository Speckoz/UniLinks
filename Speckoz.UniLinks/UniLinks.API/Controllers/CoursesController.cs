using Microsoft.AspNetCore.Mvc;

using System;
using System.Security.Claims;
using System.Threading.Tasks;

using UniLinks.API.Business.Interfaces;
using UniLinks.Dependencies.Attributes;
using UniLinks.Dependencies.Data.VO;
using UniLinks.Dependencies.Enums;

namespace UniLinks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseBusiness _courseBusiness;

        public CoursesController(ICourseBusiness courseBusiness)
        {
            _courseBusiness = courseBusiness;
        }

        [HttpGet]
        [Authorizes(UserTypeEnum.Coordinator)]
        public async Task<IActionResult> CourseByCoordIdTaskAsync()
        {
            var coordId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (await _courseBusiness.FindByCoordIdTaskAsync(coordId) is CourseVO course)
                return Ok(course);

            return NotFound("Nao existe nenhum curso com este coordenador");
        }
    }
}