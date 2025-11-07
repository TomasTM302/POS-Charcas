using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POSCharcas.Business.Mapping;
using POSCharcas.Business.Models;
using POSCharcas.Data.Repositories;

namespace POSCharcas.Business.Services
{
    /// <summary>
    /// Business logic for customer management.
    /// </summary>
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IReadOnlyList<CustomerModel>> GetAllAsync()
        {
            var entities = await _customerRepository.GetAllAsync();
            return entities.Select(e => e.ToModel()).ToList();
        }

        public async Task<CustomerModel?> GetByIdAsync(int id)
        {
            var entity = await _customerRepository.GetByIdAsync(id);
            return entity?.ToModel();
        }

        public async Task CreateAsync(CustomerModel model)
        {
            await _customerRepository.AddAsync(model.ToEntity());
        }

        public async Task UpdateAsync(CustomerModel model)
        {
            await _customerRepository.UpdateAsync(model.ToEntity());
        }

        public async Task DeleteAsync(int id)
        {
            await _customerRepository.DeleteAsync(id);
        }
    }
}
