using Contose.DAL.Core.IReposotories;
using Contoso.Models.Entities;

namespace Contose.DAL.Core.Reposotories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
