using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PayPalPaymentCreatedResponse
    {
        public string id { get; set; }
        public string intent { get; set; }
        public string state { get; set; }
        public Payer payer { get; set; }
        public Transaction[] transactions { get; set; }
        public DateTime create_time { get; set; }
        public Link[] links { get; set; }

        
    }
}
