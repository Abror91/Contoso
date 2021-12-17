using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.IServices
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentDTO>> GetDepartments();
        Task<DepartmentDTO> GetById(int id);
        Task Add(AddDepartmentViewModel model);
        Task Edit(EditDepartmentViewModel model);
        Task Delete(int id);
    }
}
