using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordLibrary.Service
{
    public class PasswordMangementService
    {
        const int keySize = 64;
        const int iterations = 350000;
        private const string saltWord = "10F28D0A3415254A0012577BD827CE7349C1EC1CA7EE7ED5FC066617E0D1DFB45546D785FA84225D221CDFEB3ABEFA3B1067920D700D266BF5B48E259821797B";
        byte[] salt = Encoding.UTF8.GetBytes(saltWord);
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        string hashedPassword;
        public PasswordMangementService() { }

        public string HashPasword(string password)
        {

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                iterations,
                hashAlgorithm,
                keySize);

            return Convert.ToHexString(hash);
        }

        public string GetHashedPassword()
        {
            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hash)
        {
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
