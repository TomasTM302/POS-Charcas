using System.Collections.Generic;

namespace POSCharcas.Data.Entities
{
    /// <summary>
    /// Entity representing customers stored in the database.
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
