using Contoso.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Models.ViewModels
{
    public class EditDepartmentViewModel
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }

        public void EditEntity(Department model)
        {
            model.Name = Name;
        }
    }
}
