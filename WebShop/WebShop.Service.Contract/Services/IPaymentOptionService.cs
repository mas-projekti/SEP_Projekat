using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Service.Contract.Dto;

namespace WebShop.Service.Contract.Services
{
    public interface IPaymentOptionService
    {
        Task<List<PaymentOptionDto>> GetAllSuportedPaymentOptions();
        Task UpdateSupportedPaymentOptions(Dictionary<string, bool> supportedPaymentOptions);

    }
}
