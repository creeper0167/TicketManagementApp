using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace TicketManagementApp.Security
{
    public class PasswordHelper
    {
        public PasswordHelper() { }

        public static string EncodePasswordMd5(string password) 
        {
            Byte[] originalBytes;
            byte[] encodeBytes;
            MD5 md5;


            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(password);
            encodeBytes = md5.ComputeHash(originalBytes);

            //return converted password
            return BitConverter.ToString(encodeBytes);
        }
    }
}