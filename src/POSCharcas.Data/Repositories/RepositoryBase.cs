using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSCharcas.Data;
using POSCharcas.Data.Configuration;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Base class that implements the common operations declared in <see cref="IRepository{TEntity}"/>.
    /// </summary>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected RepositoryBase(PosDbContextFactory contextFactory)
        {
            ContextFactory = contextFactory;
        }

        protected PosDbContextFactory ContextFactory { get; }

        protected abstract DbSet<TEntity> GetDbSet(PosDbContext context);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            await using var context = ContextFactory.CreateDbContext();
            return await GetDbSet(context).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            await using var context = ContextFactory.CreateDbContext();
            return await GetDbSet(context).FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            GetDbSet(context).Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            await using var context = ContextFactory.CreateDbContext();
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await using var context = ContextFactory.CreateDbContext();
            var set = GetDbSet(context);
            var entity = await set.FindAsync(id);
            if (entity is not null)
            {
                set.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
