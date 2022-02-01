using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Models
{
    public class BitcoinTransaction
    {
        public int Id { get; set; }
        public Guid TransactionId { get; set; }
        public string BitcoinOrderId { get; set; }
        public string PaymentUrl { get; set; }
    }
}
