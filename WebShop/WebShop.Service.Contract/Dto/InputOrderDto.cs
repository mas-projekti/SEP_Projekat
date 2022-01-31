using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShop.Service.Contract.Dto
{
    public class InputOrderDto
    {
        public string OrderStatus { get; set; }
        public DateTime? Timestamp { get; set; }
        public int CustomerId { get; set; }
        public Guid TransactionId { get; set; }
        public List<InputOrderProductDto> Products { get; set; }
    }
}
