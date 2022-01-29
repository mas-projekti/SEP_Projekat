using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class BankPaymentRequestDto
    {
        public string MerchantID { get; set; }
        public string MerchantPassword { get; set; }
        public double Amount { get; set; }
        public int MerchantOrderID { get; set; }
        public DateTime MerchantTimestamp { get; set; }
        public string SuccessURL { get; set; }
        public string ErrorURL { get; set; }
        public string FailedURL { get; set; }
    }
}
