using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Models
{
    public class PanRegistryEntry
    {
        public int Id { get; set; }
        public string PaymentCardPrefix { get; set; }
        public string BankURL { get; set; }
    }
}
