using Movies_Store.Models;

namespace Movies_Store.Data.Base
{
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(int Id, T entity);
        Task DeleteAsync(T entity);
    }
}
