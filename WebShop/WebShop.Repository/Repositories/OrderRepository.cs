using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Infrastructure;
using WebShop.Models.DomainModels;
using WebShop.Repository.Contract.Interfaces;

namespace WebShop.Repository.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(WebShopDbContext context) : base(context) { }

        public async Task<Order> GetOrderWithOrderItems(int orderId) => await _context.Orders.Include(order => order.OrderItems).ThenInclude(orderItem => orderItem.Product).FirstOrDefaultAsync(order => order.Id == orderId);

        public IEnumerable<Order> GetUserOrders(int userId) => _context.Orders.Include(x => x.OrderItems)
                                                                              .ThenInclude(ordeItem => ordeItem.Product)
                                                                              .Where(order => order.CustomerId == userId);
    }
}
