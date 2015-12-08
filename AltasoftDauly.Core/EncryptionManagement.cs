using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AltasoftDaily.Core
{
    public class EncryptionManagement
    {
        public static string CreateSalt()
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[10];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string Encrypt(string input, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(input + salt);
            var sha256Managed = new SHA256Managed();
            var encrypted = sha256Managed.ComputeHash(bytes);

            return Convert.ToBase64String(encrypted);
        }

        public static bool Validate(string sha256String, string password, string salt)
        {
            return Encrypt(password, salt) == sha256String;
        }
    }
}
