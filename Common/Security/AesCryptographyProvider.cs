﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public class AESCryptographyProvider
    {
        private string key; 
        public AESCryptographyProvider(string key)
        {
            this.key = key;
        }

        public string Decrypt(string cyphertext)
        {
            var bytes = Convert.FromBase64String(cyphertext);
            byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);
            var aes = new AesCryptoServiceProvider();
            //aes.Padding = PaddingMode.PKCS7;
            using (var memStream = new System.IO.MemoryStream(bytes))
            {
                var iv = new byte[16];
                memStream.Read(iv, 0, 16);  // Pull the IV from the first 16 bytes of the encrypted value
                using (var cryptStream = new CryptoStream(memStream, aes.CreateDecryptor(keyBytes, iv), CryptoStreamMode.Read))
                {
                    using (var reader = new System.IO.StreamReader(cryptStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

        public string Encrypt(string plaintext)
        {
            var aes = new AesCryptoServiceProvider();
            var iv = aes.IV;
            byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);
            //aes.Padding = PaddingMode.PKCS7;
            using (var memStream = new System.IO.MemoryStream())
            {
                memStream.Write(iv, 0, iv.Length);  // Add the IV to the first 16 bytes of the encrypted value
                using (var cryptStream = new CryptoStream(memStream, aes.CreateEncryptor(keyBytes, aes.IV), CryptoStreamMode.Write))
                {
                    using (var writer = new System.IO.StreamWriter(cryptStream))
                    {
                        writer.Write(plaintext);
                    }
                }
                var buf = memStream.ToArray();
                return Convert.ToBase64String(buf, 0, buf.Length);
            }
        }
    }
}
