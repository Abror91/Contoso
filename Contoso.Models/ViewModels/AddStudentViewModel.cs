using Contoso.Models.Entities;
using System;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contoso.Models.ViewModels
{
    public class AddStudentViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public int? DepartmentId { get; set; }

        public Student ToEntity()
        {
            var student = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                DepartmentId = (int)DepartmentId
            };
            return student;
        }
    }
}
