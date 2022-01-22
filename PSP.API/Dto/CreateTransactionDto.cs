using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Dto
{
    public class CreateTransactionDto
    {
        public List<ItemDto> Items { get; set; }
        public BankTransactionDto BankTransactionData { get; set; }
    }
}
