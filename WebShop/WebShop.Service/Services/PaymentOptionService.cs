using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Repository.Contract.Interfaces;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Service.Services
{
    public class PaymentOptionService : IPaymentOptionService
    {
        private readonly IPaymentOptionRepository _paymentOptionRepository;
        private readonly IMapper _mapper;

        public PaymentOptionService(IPaymentOptionRepository paymentOptionRepository, IMapper mapper)
        {
            _paymentOptionRepository = paymentOptionRepository;
            _mapper = mapper;
        }

        public async Task<List<PaymentOptionDto>> GetAllSuportedPaymentOptions()
        {
            List<PaymentOption> supported = await _paymentOptionRepository.GetAllSuportedPaymentOptions();
            return _mapper.Map<List<PaymentOptionDto>>(supported);
        }

        public async Task UpdateSupportedPaymentOptions(Dictionary<string, bool> supportedPaymentOptions)
        {
            PaymentOption option;

            foreach (KeyValuePair<string, bool> keyValuePair in supportedPaymentOptions)
            {
                option = await _paymentOptionRepository.GetPaymentOptionByName(keyValuePair.Key);

                if (option == null)
                {
                    await _paymentOptionRepository.Add(new PaymentOption() { Name = keyValuePair.Key, IsSupported = keyValuePair.Value });
                }else
                {
                    option.IsSupported = keyValuePair.Value;
                    await _paymentOptionRepository.Update(option.Id, option);
                }

            }

        }
    }
}
