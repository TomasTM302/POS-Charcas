using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSCharcas.Business.Mapping;
using POSCharcas.Business.Models;
using POSCharcas.Data.Entities;
using POSCharcas.Data.Repositories;

namespace POSCharcas.Business.Services
{
    /// <summary>
    /// Coordinates sale workflows between the UI and data layer.
    /// </summary>
    public class SalesService
    {
        private readonly SalesRepository _salesRepository;
        private readonly ProductRepository _productRepository;

        public SalesService(SalesRepository salesRepository, ProductRepository productRepository)
        {
            _salesRepository = salesRepository;
            _productRepository = productRepository;
        }

        public async Task<SaleModel> RegisterSaleAsync(IEnumerable<SaleItemModel> items, int? customerId = null)
        {
            var itemList = items.ToList();
            if (!itemList.Any())
            {
                throw new InvalidOperationException("Una venta debe contener al menos un producto.");
            }

            var sale = new Sale
            {
                Date = DateTime.Now,
                CustomerId = customerId
            };

            var entityItems = itemList.Select(i => new SaleItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();

            sale.Items = entityItems;
            var savedSale = await _salesRepository.AddSaleWithItemsAsync(sale, entityItems);
            savedSale.Items = entityItems;
            return savedSale.ToModel();
        }

        public IReadOnlyList<ProductModel> GetAvailableProducts()
        {
            var products = _productRepository.GetAllAsync().GetAwaiter().GetResult();
            return products.Select(p => p.ToModel()).ToList();
        }
    }
}
