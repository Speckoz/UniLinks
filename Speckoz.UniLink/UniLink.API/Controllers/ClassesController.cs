using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Models;

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

        // POST: /classes
        [HttpPost]
        public async Task<IActionResult> AddClassTaskAsync([FromBody]ClassModel @class)
        {
            if (ModelState.IsValid)
            {
                ClassModel newClass = await _classBusiness.AddTaskAsync(@class);
                return Created($"/classes/newClass{newClass.ClassId}", newClass);
            }

            return BadRequest();
        }

        // GET: /classes/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> FindByIdTaskAsync(Guid id)
        {
            ClassModel @class = await _classBusiness.FindByIdTaskAsync(id);
            if (@class != null) 
                return Ok(@class);

            return NoContent();
        }

        // POST: /classes/uri
        [HttpPost("uri")]
        public async Task<IActionResult> FindByURITaskAsync([FromBody]string uri)
        {
            ClassModel @class = await _classBusiness.FindByURITaskAsync(uri);
            if (@class != null)
                return Ok(@class);

            return NoContent();
        }

        // POST: /classes/date
        [HttpPost("uri")]
        public async Task<IActionResult> FindByDateTaskAsync([FromBody]DateTime date)
        {
            throw new NotImplementedException();
        }

        // POST: /classes/course
        [HttpPost("course")]
        public async Task<IActionResult> FindByCourseTaskAsync([FromBody]string course)
        {
            throw new NotImplementedException();
        }

        // PUT: /classes
        [HttpPut]
        public async Task<IActionResult> UpdateTaskAsync([FromBody]ClassModel @class)
        {
            ClassModel updatedClass = await _classBusiness.UpdateTaskAsync(@class);
            if (updatedClass != null)
                return Ok(updatedClass);

            return NoContent();
        }

        // DELETE: /classes/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAsync(Guid id)
        {
            await _classBusiness.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}