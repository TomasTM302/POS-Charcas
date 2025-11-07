using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSCharcas.Business.Mapping;
using POSCharcas.Business.Models;
using POSCharcas.Data.Repositories;

namespace POSCharcas.Business.Services
{
    /// <summary>
    /// Business logic for the product catalog.
    /// </summary>
    public class ProductService
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<ProductModel>> GetAllAsync()
        {
            var entities = await _productRepository.GetAllAsync();
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<ProductModel?> GetByIdAsync(int id)
        {
            var entity = await _productRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task CreateAsync(ProductModel model)
        {
            await _productRepository.AddAsync(model.ToEntity());
        }

        public async Task UpdateAsync(ProductModel model)
        {
            await _productRepository.UpdateAsync(model.ToEntity());
        }

        public async Task DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }

        public IReadOnlyList<ProductModel> GetAvailableProducts()
        {
            // For the prototype we expose a synchronous helper used by the picker dialog.
            return _productRepository.GetAllAsync().GetAwaiter().GetResult().Select(p => p.ToModel()).ToList();
        }
    }
}
