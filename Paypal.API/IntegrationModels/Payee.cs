using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Payee
    {
        public string merchant_id { get; set; }
        public string email { get; set; }
    }
}
