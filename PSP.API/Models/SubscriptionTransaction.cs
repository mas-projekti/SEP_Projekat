using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Models
{
    public class SubscriptionTransaction
    {
        public int Id { get; set; }
        public string SubscriptionPlanId{ get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}
