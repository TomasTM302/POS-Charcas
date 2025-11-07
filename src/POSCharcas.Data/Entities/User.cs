namespace POSCharcas.Data.Entities
{
    /// <summary>
    /// Entity that stores application users and hashed passwords.
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
