using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.IntegrationModels
{
    public class PaypalPayment
    {
        public string intent { get; set; }
        public Payer payer { get; set; }
        public Transaction[] transactions { get; set; }
        public string description { get; set; }
        public string custom { get; set; }
        public long invoice_number { get; set; }
        public PaymentOptions payment_options { get; set; }
        public string note_to_payer { get; set; }
        public string soft_descriptor { get; set; }
        public ItemList item_list { get; set; }
        public RedirectUrls redirect_urls { get; set; }

    }

}
