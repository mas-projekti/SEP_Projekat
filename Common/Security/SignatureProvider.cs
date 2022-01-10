using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Security
{
    public class SignatureProvider
    {
        private string key;

        public SignatureProvider(string key)
        {
            this.key = key;
        }

        public string SignString(string content)
        {
            string signature;
            // Initialize the keyed hash object.
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {

                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));
                signature = Convert.ToBase64String(hashValue);


            }
            return signature;
        }

        public bool IsSignatureValid(string signature, string content)
        {
            using (HMACSHA256 hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] signatureValue = Encoding.UTF8.GetBytes(signature);
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));
                signature = Encoding.UTF8.GetString(hashValue);

                for(int i = 0; i < hashValue.Length; i++)
                {
                    if (signatureValue[i] != hashValue[i])
                        return false;
                }


            }

            return true;
        }
    }
}
