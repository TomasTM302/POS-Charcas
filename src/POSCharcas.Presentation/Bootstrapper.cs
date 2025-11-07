using Microsoft.Extensions.Configuration;
using POSCharcas.Business.Services;
using POSCharcas.Data.Configuration;
using POSCharcas.Data.Repositories;

namespace POSCharcas.Presentation
{
    /// <summary>
    /// Simple bootstrapper that wires the presentation layer to the business and data layers
    /// without using a dependency injection container. This keeps the initial prototype easy to
    /// understand while keeping responsibilities separated.
    /// </summary>
    internal static class Bootstrapper
    {
        public static AuthService BuildAuthService(IConfiguration configuration)
        {
            var dbContextFactory = new PosDbContextFactory(configuration);
            var userRepository = new UserRepository(dbContextFactory);
            var authService = new AuthService(userRepository);
            return authService;
        }

        public static SalesService BuildSalesService(IConfiguration configuration)
        {
            var dbContextFactory = new PosDbContextFactory(configuration);
            var salesRepository = new SalesRepository(dbContextFactory);
            var productRepository = new ProductRepository(dbContextFactory);
            return new SalesService(salesRepository, productRepository);
        }

        public static ProductService BuildProductService(IConfiguration configuration)
        {
            var dbContextFactory = new PosDbContextFactory(configuration);
            var repository = new ProductRepository(dbContextFactory);
            return new ProductService(repository);
        }

        public static CustomerService BuildCustomerService(IConfiguration configuration)
        {
            var dbContextFactory = new PosDbContextFactory(configuration);
            var repository = new CustomerRepository(dbContextFactory);
            return new CustomerService(repository);
        }
    }
}
