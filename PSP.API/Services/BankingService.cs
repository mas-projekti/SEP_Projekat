using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PSP.API.Dto;
using PSP.API.Infrastructure;
using PSP.API.Interfaces;
using PSP.API.Models;
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

        public BankingService(IMapper mapper, PaymentServiceProviderDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<BankPaymentRequestResponseDto> InitiateBankPayment(Guid transactionID)
        {
            BankTransaction bankTransaction = _dbContext.BankTransactions.FirstOrDefault(x => x.TransactionId == transactionID);
            HttpClient bankClient = GetBankHttpClient(bankTransaction.BankURL);

            BankPaymentRequestDto paymentRequestDto = _mapper.Map<BankPaymentRequestDto>(bankTransaction);
            paymentRequestDto.SuccessURL = "nekiurl"; //TODO: Promeniti ovo da gadja moj front
            paymentRequestDto.ErrorURL = "nekiurl2"; //TODO: Promeniti ovo da gadja moj front
            paymentRequestDto.FailedURL = "nekiurl3"; //TODO: Promeniti ovo da gadja moj front

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
