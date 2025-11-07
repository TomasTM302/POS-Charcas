using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSCharcas.Data;
using POSCharcas.Data.Configuration;
using POSCharcas.Data.Entities;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Repository specialized in user queries.
    /// </summary>
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(PosDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override DbSet<User> GetDbSet(PosDbContext context) => context.Users;

        public async Task<User?> FindByUsernameAsync(string username)
        {
            await using var context = ContextFactory.CreateDbContext();
            return await context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
