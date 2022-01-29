using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class BankTransactionDto
    {
        public int Id { get; set; }
        public string MerchantID { get; set; }
        public string MerchantPassword { get; set; }
        public double Amount { get; set; }
        public int MerchantOrderID { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string BankURL { get; set; }

    }
}
