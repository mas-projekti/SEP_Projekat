using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Service.Contract.Dto;

namespace WebShop.Service.Contract.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetUserOrders(int userId);
        Task<OrderDto> GetOrderById(int orderId);
        Task<OrderDto> InsertOrder(InputOrderDto inputOrder);
        Task<OrderDto> UpdateOrderStatus(Guid transactionId);

        
        
    }
}
