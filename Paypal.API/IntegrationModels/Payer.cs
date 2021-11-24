using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Payer
    {
        public string payment_method { get; set; }
        public string status { get; set; }
        public PayerInfo payer_info { get; set; }
    }
}
