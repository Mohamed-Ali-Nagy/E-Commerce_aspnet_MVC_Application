using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Movies_Store.Models;
using System.Linq.Expressions;

namespace Movies_Store.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly CinemaContext context;
        public EntityBaseRepository(CinemaContext _context) { context = _context; }
        public async Task AddAsync(T entity)
        {
           await context.Set<T>().AddAsync(entity);
           await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            EntityEntry entry=context.Entry(entity);
            entry.State=EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

     

        public async Task<List<T>> GetAllAsync(params Expression<Func<T,object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(int Id, T entity)
        {
            EntityEntry entityEntry=context.Entry(entity);
            entityEntry.State=EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
