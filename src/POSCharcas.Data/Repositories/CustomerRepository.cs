using Microsoft.EntityFrameworkCore;
using POSCharcas.Data;
using POSCharcas.Data.Configuration;
using POSCharcas.Data.Entities;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Repository that manages customer entities.
    /// </summary>
    public class CustomerRepository : RepositoryBase<Customer>
    {
        public CustomerRepository(PosDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override DbSet<Customer> GetDbSet(PosDbContext context) => context.Customers;
    }
}
