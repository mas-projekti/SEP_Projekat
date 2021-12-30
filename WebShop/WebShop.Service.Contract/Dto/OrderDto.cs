using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Service.Contract.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? Timestamp { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
