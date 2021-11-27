using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Dto
{
    public class OrderItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public double Value { get; set; }
        public uint Quantity { get; set; }
    }
}
