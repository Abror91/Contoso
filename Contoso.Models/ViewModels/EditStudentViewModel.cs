using Contoso.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Contoso.Models.ViewModels
{
    public class EditStudentViewModel
    {
        [Required]
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        public void EditEntity(Student entity)
        {
            entity.FirstName = FirstName;
            entity.LastName = LastName;
            entity.Email = Email;
            entity.DepartmentId = (int)DepartmentId;
        }
    }
}
