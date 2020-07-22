using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Server.WebApi.Authentication
{
    public class Hasher
    {
        private const string HexFormat = "x2";

        public string Compute(string input)
        {
            using (var hash = SHA256.Create())
            {
                var hashBytes = hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                return string.Concat(hashBytes.Select(x => x.ToString(HexFormat)));
            }
        }
    }
}