using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Models.Enums;

namespace WebShop.Repository.Contract.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetUserOrders(int userId);

        Task<Order> GetOrderWithOrderItems(int orderId);

        Task<Order> UpdateOrderStatus(Guid transactionId, OrderStatus status);

    }
}
