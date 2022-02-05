using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Dto
{
    public class PaymentCardDto
    {
        public int Id { get; set; }
        public DateTime ExipiringDate { get; set; }
        public string CardNumber { get; set; }
        public string CardHolderName { get; set; }
        public string CardHolderLastName { get; set; }
        public int BankClientId { get; set; }
        public string SecurityCode { get; set; }
    }
}
