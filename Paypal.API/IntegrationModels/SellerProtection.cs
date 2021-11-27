using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class SellerProtection
    {
        public string status { get; set; }
        public string[] dispute_categories { get; set; }
    }
}
