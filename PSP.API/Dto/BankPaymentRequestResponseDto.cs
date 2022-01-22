using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class BankPaymentRequestResponseDto
    {
        public string PaymentURL { get; set; }
        public int PaymentID { get; set; }
    }
}
