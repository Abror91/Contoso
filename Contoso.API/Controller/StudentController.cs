using Contoso.Models.DTOs;
using Contoso.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.API.Controller
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IEnumerable<StudentDTO>> GetStudents()
        {
            return await _studentService.GetSetudents();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDTO>> GetById([FromRoute] int id)
        {
            var item = await _studentService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
    }
}
