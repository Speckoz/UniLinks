using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniLink.API.Attributes;
using UniLink.API.Business.Interfaces;
using UniLink.Dependencies.Enums;
using UniLink.Dependencies.Models;

namespace UniLink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;

        public StudentsController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        // GET: /students/:email
        [HttpGet("{email}")]
        public async Task<IActionResult> FindAllTaskAsync(string email)
        {
            StudentModel student = await _studentBusiness.FindByEmailTaskAsync(email);
            if (student == null)
                return BadRequest("Email não autorizado");

            return Ok(student);
        }

        // POST: /students
        [HttpPost]
        // [Authorizes(UserTypeEnum.Coordinator)]
        public async Task<IActionResult> AddStudentTaskAsync([FromBody]StudentModel student)
        {
            if (ModelState.IsValid)
            {
                StudentModel createdStudent = await _studentBusiness.AddTaskAsync(student);
                return Created("/students", createdStudent);
            }

            return BadRequest();
        }
    }
}