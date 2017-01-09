using System;
using System.Security.Cryptography;
using System.Text;

namespace FaPA.Core
{
    public static class PasswordHasher
    {

        private static readonly HashAlgorithm Algorithm = new MD5CryptoServiceProvider();


        public static string HashPassword(string username, string password)
        {
            var plainText = username + password;
            var plainTextData = Encoding.Default.GetBytes(plainText);
            var hash = Algorithm.ComputeHash(plainTextData);
            return Convert.ToBase64String(hash);
        }

    }
}