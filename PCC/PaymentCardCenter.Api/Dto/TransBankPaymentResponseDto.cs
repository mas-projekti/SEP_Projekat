using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Dto
{
    public class TransBankPaymentResponseDto
    {
        public string PaymentCardNumber { get; set; }
        public int AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
        public int IssuerOrderId { get; set; }
        public DateTime IssuerTimestamp { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
