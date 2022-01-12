
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paypal.API.Dto;
using Paypal.API.Infrastructure;
using Paypal.API.IntegrationModels;
using Paypal.API.Interfaces;
using Paypal.API.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Paypal.API.Services
{
    
    public class PaypalService :IPaypalService
    {
        private readonly PaypalOptions _paypalOptions;
        private HttpClient paypalClient;
        private const string sandbox = "https://api-m.sandbox.paypal.com";
        private readonly IDataAdapter dataAdapter;
        private readonly ILogger<PaypalService> _logger;
        private readonly PaypalDbContext _dbContext;
        public PaypalService(IOptions<PaypalOptions> paypalOptions, IDataAdapter dataAdapter, ILogger<PaypalService> logger, PaypalDbContext dbContext)
        {
            _paypalOptions = paypalOptions.Value;
            paypalClient = GetPaypalHttpClient();
            this.dataAdapter = dataAdapter;
            _logger = logger;
            _dbContext = dbContext;

        }

        private HttpClient GetPaypalHttpClient()
        {

            var http = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

        public async Task<PayPalAccessToken> GetPayPalAccessTokenAsync()
        {
            _logger.LogInformation("Getting token from paypal, to server request.");
            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{_paypalOptions.PayPalClientId}:{_paypalOptions.PayPalClientSecret}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);

            HttpResponseMessage response = await paypalClient.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);
            return accessToken;
        }

        

        public async Task<PaypalOrderCreatedResponse> CreatePaypalOrderAsync(CreateOrderDto order)
        {
            _logger.LogInformation($"Creating order with {order.Items.Count} items.");
            PayPalAccessToken token = await GetPayPalAccessTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/checkout/orders");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            JObject orderObj = dataAdapter.RepackOrderToJObject(order);

            request.Content = new StringContent(JsonConvert.SerializeObject(orderObj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await paypalClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PaypalOrderCreatedResponse createdOrder = JsonConvert.DeserializeObject<PaypalOrderCreatedResponse>(content);
            if(createdOrder.id == null)
            {
                _logger.LogError($"Order was not created, Paypal returned {createdOrder.links[0].href}");
            }
            else
            {
                _logger.LogInformation($"Order with id {createdOrder.id} created.");
                _dbContext.PaypalTransaction.Add(new Models.PaypalTransaction {
                    TransactionId = order.TransactionID,
                    PaypalOrderId = createdOrder.id
                    });
                _dbContext.SaveChanges();

            }
            return createdOrder;
        }

        //SYNC
        public async Task<PaypalOrderCapturedResponse> CapturePaypalOrderAsync(string orderId)
        {
            _logger.LogInformation($"Capturing order with ID = {orderId}.");
            PayPalAccessToken token = await GetPayPalAccessTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/checkout/orders/{orderId}/capture");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            request.Content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await paypalClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PaypalOrderCapturedResponse createdOrder = JsonConvert.DeserializeObject<PaypalOrderCapturedResponse>(content);
            if (createdOrder.status == "FAILED")
            {
                _logger.LogError($"Order was not completed, Paypal returned {createdOrder.links[0].href}");
            }
            else
            {
                _logger.LogInformation($"Order with id {createdOrder.id} captured.");

                //Create client
                var http = new HttpClient
                {
                    BaseAddress = new Uri("https://localhost:44313"),
                    Timeout = TimeSpan.FromSeconds(30),
                };
                Guid transId = _dbContext.PaypalTransaction.FirstOrDefault(x => x.PaypalOrderId == orderId).TransactionId;

                HttpRequestMessage requestM = new HttpRequestMessage(HttpMethod.Post, $"/payment-service/clients/notify/{transId}");
                requestM.Content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8, "application/json");

                HttpResponseMessage responseM = await http.SendAsync(requestM);

            }


            return createdOrder;

        }
    }
}
