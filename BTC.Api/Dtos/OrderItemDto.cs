using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Dtos
{
    public class OrderItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public uint Quantity { get; set; }
    }
}
