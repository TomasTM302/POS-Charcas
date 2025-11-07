using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using POSCharcas.Data.Repositories;

namespace POSCharcas.Business.Services
{
    /// <summary>
    /// Provides authentication helpers such as validating credentials.
    /// </summary>
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Validates a set of credentials by hashing the provided password and comparing it with
        /// the stored value.
        /// </summary>
        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = await _userRepository.FindByUsernameAsync(username);
            if (user is null)
            {
                return false;
            }

            string hashed = HashPassword(password);
            return string.Equals(user.PasswordHash, hashed, System.StringComparison.Ordinal);
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return System.Convert.ToHexString(bytes);
        }
    }
}
