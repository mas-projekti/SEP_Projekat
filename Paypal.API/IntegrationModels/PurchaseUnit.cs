using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PurchaseUnit
    {
        public string reference_id { get; set; }
        public string invoice_id { get; set; }
        public Shipping shipping { get; set; }
        public Payments payments { get; set; }
       

    }
}
