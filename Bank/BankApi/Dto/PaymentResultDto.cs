using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class PaymentResultDto
    {
        public int PaymentID { get; set; }
        public bool IsSuccessFull { get; set; }
        public string MerchantID { get; set; }
        public bool IsWithinSameBank { get; set; }
        public string AcquirerOrderID { get; set; }
        public DateTime AcquirerTimestamp { get; set; }

    }
}
