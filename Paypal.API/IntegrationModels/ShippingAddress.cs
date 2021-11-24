using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class ShippingAddress
    {
        public string recipient_name { get; set; }
        public string line1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
        public string country_code { get; set; }
    }
}
