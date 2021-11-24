using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class ItemList
    {
        public Item[] items { get; set; }
        public ShippingAddress shipping_address { get; set; }
    }
}
