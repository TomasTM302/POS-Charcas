using System;
using System.Collections.Generic;
using System.Linq;

namespace POSCharcas.Business.Models
{
    /// <summary>
    /// Business representation of a sale including its items and aggregated totals.
    /// </summary>
    public class SaleModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public int? CustomerId { get; set; }
        public IReadOnlyCollection<SaleItemModel> Items { get; set; } = Array.Empty<SaleItemModel>();
        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
