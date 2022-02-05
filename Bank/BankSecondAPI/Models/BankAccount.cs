using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public int BankClientId { get; set; }
        public BankClient BankClient { get; set; }
        public double MoneyAmount { get; set; }
        public double ReservedMoneyAmount { get; set; }
    }
}
