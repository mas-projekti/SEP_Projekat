using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Options
{
    public class EncryptionKeyOptions
    {
        public static string EncryptionKey = "EncryptionKey";
        public string Key { get; set; }

        public EncryptionKeyOptions()
        {
        }
    }
}
