using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Dto
{
    public class CreateOrderDto
    {
        public string MerchantID { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
