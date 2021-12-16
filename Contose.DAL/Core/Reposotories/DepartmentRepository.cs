using Contose.DAL.Core.IReposotories;
using Contoso.Models.Entities;

namespace Contose.DAL.Core.Reposotories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
