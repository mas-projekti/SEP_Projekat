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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(WebShopDbContext context) : base(context) { }

        public async Task<Product> UpdateProductAmount(int productId, int amount)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);

            if (product == null)
                throw new Exception($"Cannot find product with id = {productId}");

            if (product.Amount - amount >= 0)
            {
                product.Amount -= amount;
            } else
            {
                product.Amount = 0;
            }

            await _context.SaveChangesAsync();

            return product;

        }


    }
}
