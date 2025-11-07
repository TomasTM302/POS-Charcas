using Microsoft.EntityFrameworkCore;
using POSCharcas.Data.Entities;

namespace POSCharcas.Data
{
    /// <summary>
    /// Entity Framework database context that maps the domain entities to SQL Server tables.
    /// </summary>
    public class PosDbContext : DbContext
    {
        public PosDbContext(DbContextOptions<PosDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<SaleItem> SaleItems => Set<SaleItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<SaleItem>().Property(i => i.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Sale!)
                .HasForeignKey(i => i.SaleId);
        }
    }
}
