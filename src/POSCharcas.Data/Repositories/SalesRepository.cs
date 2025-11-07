using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using POSCharcas.Data;
using POSCharcas.Data.Configuration;
using POSCharcas.Data.Entities;

namespace POSCharcas.Data.Repositories
{
    /// <summary>
    /// Repository that persists sales headers and their details.
    /// </summary>
    public class SalesRepository : RepositoryBase<Sale>
    {
        public SalesRepository(PosDbContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        protected override DbSet<Sale> GetDbSet(PosDbContext context) => context.Sales;

        public async Task<Sale> AddSaleWithItemsAsync(Sale sale, IEnumerable<SaleItem> items)
        {
            await using var context = ContextFactory.CreateDbContext();
            await context.Sales.AddAsync(sale);
            await context.SaleItems.AddRangeAsync(items);
            await context.SaveChangesAsync();
            return sale;
        }
    }
}
