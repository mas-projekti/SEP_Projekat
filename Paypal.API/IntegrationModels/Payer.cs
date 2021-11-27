using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Payer
    {
        public string email_address { get; set; }
        public string payer_id { get; set; }
        public Name name { get; set; }
    }
}
