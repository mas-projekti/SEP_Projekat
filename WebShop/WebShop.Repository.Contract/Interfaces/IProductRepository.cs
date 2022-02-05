using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Repository.Contract.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<Product> UpdateProductAmount(int productId, int amount);
    }
}
