using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Services.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentDTO> GetById(int id)
        {
            var entity = await _studentRepository.GetById(id);
            return entity.ToStudentDTO();
        }

        public async Task<IEnumerable<StudentDTO>> GetSetudents()
        {
            var entities = await _studentRepository.GetAll();
            var items = new List<StudentDTO>();
            foreach(var s in entities)
            {
                items.Add(s.ToStudentDTO());
            }
            return items;
        }
    }
}
