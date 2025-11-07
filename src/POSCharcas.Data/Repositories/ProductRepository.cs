using Microsoft.EntityFrameworkCore;
using POSCharcas.Data;
using POSCharcas.Data.Configuration;
using POSCharcas.Data.Entities;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Repository that exposes CRUD operations for products.
    /// </summary>
    public class ProductRepository : RepositoryBase<Product>
    {
        public ProductRepository(PosDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override DbSet<Product> GetDbSet(PosDbContext context) => context.Products;
    }
}
