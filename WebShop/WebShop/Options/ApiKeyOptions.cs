using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebShop.Options
{
    public class ApiKeyOptions
    {
        public const string HookSecret = "ApiKey";
        public string Key { get; set; }
    }
}
