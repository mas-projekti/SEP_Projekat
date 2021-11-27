using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Payments
    {
        public Capture[] captures { get; set; }
    }
}
