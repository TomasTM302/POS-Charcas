namespace POSCharcas.Business.Models
{
    /// <summary>
    /// Business layer representation of a customer.
    /// </summary>
    public class CustomerModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public CustomerModel Clone() => (CustomerModel)MemberwiseClone();
    }
}
