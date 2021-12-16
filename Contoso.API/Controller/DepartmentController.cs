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
        public async Task<IActionResult> Add([FromBody] AddDepartmentViewModel model)
        {
            await _departmentService.Add(model);

            return Ok();
        }
    }
}
