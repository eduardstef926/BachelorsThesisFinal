using System.Security.Cryptography;
using System.Text;

namespace NewBackend2.Helpers
{
    public class PasswordService
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "");
                return hashedPassword;
            }
        }
    }
}
