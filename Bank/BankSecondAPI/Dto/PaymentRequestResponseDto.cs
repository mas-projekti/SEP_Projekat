using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class PaymentRequestResponseDto
    {
        public string PaymentURL { get; set; }
        public int PaymentID { get; set; }
    }
}
