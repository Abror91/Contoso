using Contoso.Models.DTOs;
using System.Collections.Generic;

namespace Contoso.Models.Entities
{
    public class Department : BaseModel
    {
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }

        public DepartmentDTO ToDepartmentDTO()
        {
            var department = new DepartmentDTO
            {
                Id = Id,
                Name = Name
            };
            return department;
        }
    }
}
