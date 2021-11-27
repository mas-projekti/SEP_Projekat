using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class SellerReceivableBreakdown
    {
        public NetAmount gross_amount { get; set; }
        public NetAmount net_amount { get; set; }
        public NetAmount paypal_amount { get; set; }
    }
}
