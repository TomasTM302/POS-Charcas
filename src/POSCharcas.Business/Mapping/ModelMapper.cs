using System.Collections.Generic;
using System.Linq;
using POSCharcas.Business.Models;
using POSCharcas.Data.Entities;

namespace POSCharcas.Business.Mapping
{
    /// <summary>
    /// Centralizes conversions between data layer entities and business layer models.
    /// </summary>
    public static class ModelMapper
    {
        public static ProductModel ToModel(this Product entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Price = entity.Price,
            Stock = entity.Stock,
            Description = entity.Description
        };

        public static Product ToEntity(this ProductModel model) => new()
        {
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            Stock = model.Stock,
            Description = model.Description
        };

        public static CustomerModel ToModel(this Customer entity) => new()
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Email = entity.Email,
            PhoneNumber = entity.PhoneNumber,
            Address = entity.Address
        };

        public static Customer ToEntity(this CustomerModel model) => new()
        {
            Id = model.Id,
            FullName = model.FullName,
            Email = model.Email,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address
        };

        public static SaleModel ToModel(this Sale entity) => new()
        {
            Id = entity.Id,
            Date = entity.Date,
            CustomerId = entity.CustomerId,
            Items = entity.Items.Select(i => i.ToModel()).ToList()
        };

        public static SaleItemModel ToModel(this SaleItem entity) => new()
        {
            ProductId = entity.ProductId,
            ProductName = entity.Product?.Name ?? string.Empty,
            Quantity = entity.Quantity,
            Price = entity.Price
        };

        public static Sale ToEntity(this SaleModel model) => new()
        {
            Id = model.Id,
            Date = model.Date,
            CustomerId = model.CustomerId,
            Items = model.Items.Select(i => i.ToEntity()).ToList()
        };

        public static SaleItem ToEntity(this SaleItemModel model) => new()
        {
            ProductId = model.ProductId,
            Quantity = model.Quantity,
            Price = model.Price
        };

        public static IReadOnlyList<ProductModel> ToModels(this IEnumerable<Product> entities) => entities.Select(e => e.ToModel()).ToList();
        public static IReadOnlyList<CustomerModel> ToModels(this IEnumerable<Customer> entities) => entities.Select(e => e.ToModel()).ToList();
    }
}
