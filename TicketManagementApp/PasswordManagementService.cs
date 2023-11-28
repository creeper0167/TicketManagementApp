using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketManagementApp
{
    public class PasswordManagementService
    {
        const int keySize = 64;
        const int iterations = 350000;
        HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
        byte[] salt = Encoding.UTF8.GetBytes("10F28D0A3415254A0012577BD827CE7349C1EC1CA7EE7ED5FC066617E0D1DFB45546D785FA84225D221CDFEB3ABEFA3B1067920D700D266BF5B48E259821797B");
        //string HashPasword(string password)
        //{

        //    var hash = Rfc2898DeriveBytes.Pbkdf2(
        //        Encoding.UTF8.GetBytes(password),
        //        salt,
        //        iterations,
        //        hashAlgorithm,
        //        keySize);

        //    return Convert.ToHexString(hash);
        //}
    }
}