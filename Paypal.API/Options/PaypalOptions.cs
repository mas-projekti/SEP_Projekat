using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Options
{
    public class PaypalOptions
    {
        public const string Paypal = "Paypal";
        public string PayPalClientId { get; set; }
        public string PayPalClientSecret { get; set; }

        public PaypalOptions()
        {
        }

       
    }
}
