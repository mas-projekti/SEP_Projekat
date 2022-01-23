using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class TransBankPaymentRequestDto
    {
        public string PaymentCardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderLastName { get; set; }
        public string SecurityCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public double Amount { get; set; }
        public int AcquirerOrderId { get; set; }
        public DateTime AcquirerTimestamp { get; set; }
    }
}
