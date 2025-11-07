namespace POSCharcas.Business.Models
{
    /// <summary>
    /// DTO that represents a product inside the business layer. It can be mapped from and to
    /// the entity model defined in the data layer.
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;

        public ProductModel Clone() => (ProductModel)MemberwiseClone();
    }
}
