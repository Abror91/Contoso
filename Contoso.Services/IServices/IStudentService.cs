using Contoso.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetSetudents();
        Task<StudentDTO> GetById(int id);
    }
}
