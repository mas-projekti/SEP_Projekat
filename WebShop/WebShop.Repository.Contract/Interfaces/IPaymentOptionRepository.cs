using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Repository.Contract.Interfaces
{
    public interface IPaymentOptionRepository : IGenericRepository<PaymentOption>
    {
        Task<List<PaymentOption>> GetAllSuportedPaymentOptions();
        Task<PaymentOption> GetPaymentOptionByName(string name);
    }
}
