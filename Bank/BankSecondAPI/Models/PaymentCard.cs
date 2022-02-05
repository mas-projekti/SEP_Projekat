using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class PaymentCard
    {
        public int Id { get; set; }
        public DateTime ExipiringDate { get; set; }
        public string CardNumber { get; set; }
        public BankClient BankClient { get; set; }
        public int BankClientId { get; set; }
        public string SecurityCode { get; set; }
    }
}
