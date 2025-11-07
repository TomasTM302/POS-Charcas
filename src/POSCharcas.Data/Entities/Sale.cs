using System;
using System.Collections.Generic;

namespace POSCharcas.Data.Entities
{
    /// <summary>
    /// Entity that represents a sale transaction header.
    /// </summary>
    public class Sale
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<SaleItem> Items { get; set; } = new List<SaleItem>();
    }
}
