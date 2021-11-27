using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class Capture
    {
        public string id { get; set; }
        public string status { get; set; }
        public bool final_capture { get; set; }
        public string disbursement_mode { get; set; }
        public DateTime create_time { get; set; }
        public DateTime update_time { get; set; }
        public Link[] links { get; set; }
        public SellerReceivableBreakdown seller_receivable_breakdown { get; set; }
        public NetAmount amount { get; set; }
        public SellerProtection seller_protection { get; set; }
    }
}
