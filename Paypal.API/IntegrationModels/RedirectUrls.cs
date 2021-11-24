using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class RedirectUrls
    {
        public string return_url { get; set; }
        public string cancel_url { get; set; }
    }
}
