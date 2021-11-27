using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PaypalOrderCapturedResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public Payer payer;
        public PurchaseUnit[] purchase_units { get; set; }
        public Link[] links;
    }
}
