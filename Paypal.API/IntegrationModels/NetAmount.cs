using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class NetAmount
    {
        public string currency_code { get; set; }
        public double value { get; set; }
    }
}
