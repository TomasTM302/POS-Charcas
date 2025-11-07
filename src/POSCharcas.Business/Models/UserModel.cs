namespace POSCharcas.Business.Models
{
    /// <summary>
    /// Business representation of an application user.
    /// </summary>
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
