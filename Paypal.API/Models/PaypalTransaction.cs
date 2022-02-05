using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Models
{
    public class PaypalTransaction
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string PaypalOrderId { get; set; }
    }
}
