using Contoso.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contose.DAL.Core.IReposotories
{
    public interface IGenericReposotiry<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void Edit(T entity);
        Task Delete(int id);
        Task SaveChanges();
    }
}
