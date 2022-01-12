using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Services
{
    public class SignatureVerifierService
    {
        private string key;

        public SignatureVerifierService(string key)
        {
            this.key = key;
        }


        public bool IsSignatureValid(string signature, string content)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] signatureValue = Encoding.UTF8.GetBytes(signature);
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));
                string signatureVerified = Convert.ToBase64String(hashValue);

                for (int i = 0; i < signatureVerified.Length; i++)
                {
                    if (signature[i] != signatureVerified[i])
                        return false;
                }


            }

            return true;
        }
    }
}
