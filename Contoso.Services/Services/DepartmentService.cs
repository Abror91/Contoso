using Contose.DAL.Core.IReposotories;
using Contoso.Models.DTOs;
using Contoso.Models.Entities;
using Contoso.Models.ViewModels;
using Contoso.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var entity = new Department { Name = model.Name };
            await _departmentRepository.Add(entity);
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
