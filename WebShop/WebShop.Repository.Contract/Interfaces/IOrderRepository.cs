using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Repository.Contract.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        IEnumerable<Order> GetUserOrders(int userId);

        Task<Order> GetOrderWithOrderItems(int orderId);

    }
}
