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
    public class PaymentOptionRepository : GenericRepository<PaymentOption>, IPaymentOptionRepository
    {
        public PaymentOptionRepository(WebShopDbContext context) : base(context) { }

        public async Task<List<PaymentOption>> GetAllSuportedPaymentOptions()
        {
            return await _context.PaymentOptions.Where(x => x.IsSupported == true).ToListAsync();
        }

        public async Task<PaymentOption> GetPaymentOptionByName(string name)
        {
            return await _context.PaymentOptions.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}
