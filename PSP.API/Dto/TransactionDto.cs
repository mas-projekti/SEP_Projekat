using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class TransactionDto
    {
        public Guid Id { get; set; }
        public List<ItemDto> Items { get; set; }
        public List<string> MerchantIds { get; set; }
        public string Currency { get; set; }
    }
}
