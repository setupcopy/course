using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CourseUtility
{
    public class PasswordHasher
    {
        public static string HashPassword(string value,string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                password:value,//password
                salt:Encoding.UTF8.GetBytes(salt),//salt
                prf:KeyDerivationPrf.HMACSHA512,//sha-512 random number
                iterationCount:10000,// iteration count 迭代次数 
                numBytesRequested:254 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        public static string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public static bool VerifyHashedPassword(string value,string salt,string storeHash)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (string.IsNullOrEmpty(storeHash))
            {
                throw new ArgumentNullException(nameof(storeHash));
            }

            var hashResult = HashPassword(value, salt);

            return hashResult == storeHash;
        }
    }
}
