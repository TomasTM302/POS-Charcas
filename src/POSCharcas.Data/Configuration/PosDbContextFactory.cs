using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace POSCharcas.Data.Configuration
{
    /// <summary>
    /// Factory that builds new DbContext instances based on configuration settings.
    /// </summary>
    public class PosDbContextFactory
    {
        private readonly IConfiguration _configuration;

        public PosDbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PosDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PosDbContext>();
            var connectionString = _configuration.GetConnectionString("PosDatabase")
                ?? "Server=.;Database=PosCharcas;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
            return new PosDbContext(optionsBuilder.Options);
        }
    }
}
