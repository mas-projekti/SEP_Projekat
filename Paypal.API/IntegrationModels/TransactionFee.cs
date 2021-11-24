using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class TransactionFee
    {
        public string value { get; set; }
        public string currency { get; set; }
    }
}
