using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PaypalOrderCreatedResponse
    {
        public string id { get; set; }
        public string status { get; set; }
        public Link[] links { get; set; }
    }
}
