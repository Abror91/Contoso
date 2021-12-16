using Contose.DAL.Core.IReposotories;
using Contoso.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contose.DAL.Core.Reposotories
{
    public abstract class GenericRepository<T> : IGenericReposotiry<T>, IDisposable where T : BaseModel
    {
        protected ApplicationDbContext _context;
        protected DbSet<T> dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await dbSet.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public virtual async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual void Edit(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(int id)
        {
            var entity = await dbSet.FindAsync(id);
            Delete(entity);
        }

        public virtual void Delete(T entity)
        {
            if(_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
