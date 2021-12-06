using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public long quantity { get; set; }
        public NetAmount amount { get; set; }
    }
}
