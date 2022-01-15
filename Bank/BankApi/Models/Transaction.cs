using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int PaymentId { get; set; }
    }
}
