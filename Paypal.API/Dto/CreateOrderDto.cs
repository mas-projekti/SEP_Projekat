using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Dto
{
    public class CreateOrderDto
    {
        public string CancelUrl { get; set; }
        public string ReturnUrl { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}
