using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Models.ViewModels;
using Contoso.Services.IServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task Add(AddDepartmentViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            await _departmentRepository.Add(model.ToEntity());
            await _departmentRepository.SaveChanges();
        }

        public async Task Delete(int id)
        {
            await _departmentRepository.Delete(id);
            await _departmentRepository.SaveChanges();
        }

        public async Task Edit(EditDepartmentViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var entity = await _departmentRepository.GetById((int)model.Id);

            if (entity == null)
                throw new Exception("Department not found!");

            model.EditEntity(entity);
            _departmentRepository.Edit(entity);
            await _departmentRepository.SaveChanges();
        }

        public async Task<DepartmentDTO> GetById(int id)
        {
            var entity = await _departmentRepository.GetById(id);
            return entity.ToDepartmentDTO();
        }

        public async Task<IEnumerable<DepartmentDTO>> GetDepartments()
        {
            var entities = await _departmentRepository.GetAll();
            var items = new List<DepartmentDTO>();
            foreach(var s in entities)
            {
                items.Add(s.ToDepartmentDTO());
            }
            return items;
        }
    }
}
