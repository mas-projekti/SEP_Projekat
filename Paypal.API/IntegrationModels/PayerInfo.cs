using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PayerInfo
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string payer_id { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public string country_code { get; set; }
        public BillingAddress billing_address { get; set; }
    }
}
