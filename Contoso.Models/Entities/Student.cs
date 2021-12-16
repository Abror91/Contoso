using Contoso.Models.DTOs;

namespace Contoso.Models.Entities
{
    public class Student : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public StudentDTO ToStudentDTO()
        {
            var student = new StudentDTO
            {
                Id = Id,
                Fullname = $"{FirstName} {LastName}",
                Email = Email
            };
            return student;
        }
    }
}
