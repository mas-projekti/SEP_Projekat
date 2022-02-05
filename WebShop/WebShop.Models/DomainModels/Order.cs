using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.Enums;

namespace WebShop.Models.DomainModels
{
    public class Order
    {
        public int Id { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? Timestamp { get; set; }
        public int CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public User Customer { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
