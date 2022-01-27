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
        public int MerchantOrderID { get; set; }
        public bool IsWithinSameBank { get; set; }
        public int? AcquirerOrderID { get; set; }
        public DateTime? AcquirerTimestamp { get; set; }
        public string SuccessURL { get; set; }
        public string ErrorURL { get; set; }
        public string FailedURL { get; set; }

    }
}
