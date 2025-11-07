using System.Collections.Generic;

namespace POSCharcas.Data.Entities
{
    /// <summary>
    /// Entity Framework model for the product table.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;

        public ICollection<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
    }
}
