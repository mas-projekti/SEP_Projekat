using AutoMapper;
using Common.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Models;
using PSP.API.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PSP.API.Services
{
    public class BankingService : IBankingService
    {
        private readonly IMapper _mapper;
        private readonly PaymentServiceProviderDbContext _dbContext;
        private readonly HookSecretOptions _hookOptions;

        public BankingService(IMapper mapper, PaymentServiceProviderDbContext dbContext, IOptions<HookSecretOptions> hookOptions)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _hookOptions = hookOptions.Value;
        }

        public async Task<BankPaymentRequestResponseDto> InitiateBankPayment(Guid transactionID)
        {
            BankTransaction bankTransaction = _dbContext.BankTransactions.FirstOrDefault(x => x.TransactionId == transactionID);
            HttpClient bankClient = GetBankHttpClient(bankTransaction.BankURL);

            AESCryptographyProvider provider = new AESCryptographyProvider(_hookOptions.Key);
            BankPaymentRequestDto paymentRequestDto = _mapper.Map<BankPaymentRequestDto>(bankTransaction);
            paymentRequestDto.MerchantPassword = provider.Decrypt(paymentRequestDto.MerchantPassword);
            paymentRequestDto.SuccessURL = $"http://localhost:3000/transaction-passed/{transactionID}";
            paymentRequestDto.ErrorURL = "http://localhost:3000/transaction-error"; 
            paymentRequestDto.FailedURL = "http://localhost:3000/transaction-failed"; 

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"api/payments");
            request.Content = new StringContent(JsonConvert.SerializeObject(paymentRequestDto), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await bankClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            BankPaymentRequestResponseDto createdPayment = JsonConvert.DeserializeObject<BankPaymentRequestResponseDto>(content);

            return createdPayment;
        }

        private HttpClient GetBankHttpClient(string bankURL)
        {

            var http = new HttpClient
            {
                BaseAddress = new Uri(bankURL),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }
    }
}
