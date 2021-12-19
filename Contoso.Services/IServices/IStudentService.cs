using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetSetudents();
        Task<StudentDTO> GetById(int id);
        Task<int> Add(AddStudentViewModel model);
        Task Edit(EditStudentViewModel model);
        Task Delete(int id);
    }
}
