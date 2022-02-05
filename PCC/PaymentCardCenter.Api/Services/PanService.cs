using AutoMapper;
using Newtonsoft.Json;
using PaymentCardCenter.Api.Dto;
using PaymentCardCenter.Api.Infrastructure;
using PaymentCardCenter.Api.Interfaces;
using PaymentCardCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Services
{
    public class PanService : IPanService
    {
        private readonly PccDbContext _dbContext;

        public PanService(PccDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TransBankPaymentResponseDto> RedirectPaymentToIssuer(TransBankPaymentRequestDto request)
        {
            PanRegistryEntry entry = _dbContext.Entries.FirstOrDefault(x => x.PaymentCardPrefix == request.PaymentCardNumber.Substring(0, 4));
            if (entry == null)
                throw new Exception("Card is not issued by any valid bank");

            var http = new HttpClient
            {
                BaseAddress = new Uri(entry.BankURL),
                Timeout = TimeSpan.FromSeconds(60),
            };

            HttpRequestMessage redirectRequest = new HttpRequestMessage(HttpMethod.Post, $"api/payments/external");


            redirectRequest.Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(redirectRequest);
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(content);
            TransBankPaymentResponseDto responseDto= JsonConvert.DeserializeObject<TransBankPaymentResponseDto>(content);
            return responseDto;
        }
    }
}
