using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
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

        public async Task<int> Add(AddStudentViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var id = await _studentRepository.Add(model.ToEntity());
            return id;
        }

        public async Task Delete(int id)
        {
            await _studentRepository.Delete(id);
            await _studentRepository.SaveChanges();
        }

        public async Task Edit(EditStudentViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var entity = await _studentRepository.GetById((int)model.Id);

            if (entity == null)
                throw new Exception("Student not found!");

            model.EditEntity(entity);
            _studentRepository.Edit(entity);
            await _studentRepository.SaveChanges();
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
