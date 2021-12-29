using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Contract.Dto
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public ProductDto Product { get; set; }
        public int OrderId { get; set; }
    }
}
