using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Dtos
{
    public class BitcoinOrderDto
    {
        public Guid TransactionId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public string Email { get; set; }
        public string MerchantId { get; set; }
    }
}
