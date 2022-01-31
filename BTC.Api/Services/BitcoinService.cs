using BTC.Api.Dtos;
using BTC.Api.Infrastructure;
using BTC.Api.Interfaces;
using BTC.Api.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BTC.Api.Services
{
    public class BitcoinService : IBitcoinService
    {
        private const string _coingate_sandbox = "https://api-sandbox.coingate.com";
        private const string _coingateToken = "CoingateToken";
        private HttpClient httpClient;
        private readonly IDataConverter dataConverter;
        private readonly IConfiguration _config;
        private readonly ILogger<BitcoinService> _logger;
        private readonly BitcoinDbContext _dbContext;

        public BitcoinService(IDataConverter dataConverter, IConfiguration configuration, ILogger<BitcoinService> logger, BitcoinDbContext dbContext)
        {
            this.httpClient = GetPaypalHttpClient();
            this.dataConverter = dataConverter;
            this._config = configuration;
            this._logger = logger;
            this._dbContext = dbContext;

        }

        public async Task<BitcoinOrderResult> CreatePaypalOrderAsync(BitcoinOrderDto order)
        {

            string _token = _config.GetValue<string>(_coingateToken);

            _logger.LogInformation($"{nameof(BitcoinService)}::{nameof(CreatePaypalOrderAsync)}::Sedning request to Coingate API...");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/orders");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            JObject orderObj = await dataConverter.RepackOrderToJObjectAsync(order);
            request.Content = new StringContent(JsonConvert.SerializeObject(orderObj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"{nameof(BitcoinService)}::{nameof(CreatePaypalOrderAsync)}::Recieved data from Coingate API.");

            BitcoinOrderResult createdOrder = JsonConvert.DeserializeObject<BitcoinOrderResult>(content);
            if (createdOrder == null || createdOrder.Id == 0)
            {
                _logger.LogError($"Order was not created!");
            }
            else
            {
                _logger.LogInformation($"Order with id {createdOrder.Id} created.");
                _dbContext.BitcoinTransactions.Add(new BitcoinTransaction
                {
                    TransactionId = order.TransactionId,
                    BitcoinOrderId = createdOrder.Id.ToString(),
                    PaymentUrl = createdOrder.Payment_url
                });
                _dbContext.SaveChanges();

            }
            return createdOrder;
        }


      
        private HttpClient GetPaypalHttpClient()
        {

            var http = new HttpClient
            {
                BaseAddress = new Uri(_coingate_sandbox),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

      
    }
}
