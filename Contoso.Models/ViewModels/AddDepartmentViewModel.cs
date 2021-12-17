using Contoso.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Models.ViewModels
{
    public class AddDepartmentViewModel
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 2)]
        public string Name { get; set; }

        public Department ToEntity()
        {
            var department = new Department
            {
                Name = Name
            };
            return department;
        }
    }
}
