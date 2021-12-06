using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
        public double Value { get; set; }
        public string MerchantId { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }

    }
}
