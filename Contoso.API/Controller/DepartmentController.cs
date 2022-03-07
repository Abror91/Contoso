using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
using Contoso.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.API.Controller
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            return await _departmentService.GetDepartments();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDTO>> GetById([FromRoute] int id)
        {
            var item = await _departmentService.GetById(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Add([FromBody] AddDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            var id = await _departmentService.Add(model);

            return Ok(id);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] EditDepartmentViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is not valid");

            if (id != model.Id)
                return BadRequest("Department Ids do not match");

            await _departmentService.Edit(model);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _departmentService.Delete(id);
            return NoContent();
        }

    }
}
