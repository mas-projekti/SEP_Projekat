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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(WebShopDbContext context) : base(context) { }
    }
}
