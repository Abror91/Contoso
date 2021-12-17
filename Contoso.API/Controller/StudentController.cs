using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Add([FromBody] AddStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            await _studentService.Add(model);

            return NoContent();
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditStudentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            if (id != model.Id)
                return BadRequest("Student Ids do not match");

            await _studentService.Edit(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _studentService.Delete(id);
            return NoContent();
        }
    }
}
