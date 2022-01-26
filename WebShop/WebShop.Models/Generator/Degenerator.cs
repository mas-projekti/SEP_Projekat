using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Models.Generator
{
    public class Degenerator
    {
        private static Random random = new Random();

        private const int LENGTH = 12;

        private static MD5 md5 = MD5.Create();

        public static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, LENGTH)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GeneratePasswordWithSalt(string passAndSalt)
        {
            byte[] byteArray = md5.ComputeHash(Encoding.UTF8.GetBytes(passAndSalt));
            string hashedPassword = BitConverter.ToString(byteArray, 0, byteArray.Length).Replace("-", "");
            return hashedPassword;

        }
    }
}
