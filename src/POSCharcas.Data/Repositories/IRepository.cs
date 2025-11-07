using System.Collections.Generic;
using System.Threading.Tasks;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Basic contract for CRUD repositories using asynchronous operations.
    /// </summary>
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
    }
}
