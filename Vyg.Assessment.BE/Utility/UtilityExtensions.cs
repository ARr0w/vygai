using System.Security.Cryptography;
using System.Text;

namespace Vyg.Assessment.BE.Utility
{
    public static class UtilityExtensions
    {
        public static string ConvertToHash(this string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2")); 
                }
                return builder.ToString();
            }
        }
    }
}
