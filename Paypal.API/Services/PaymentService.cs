
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    
    public class PaymentService :IPaymentService
    {
        private readonly PaypalOptions _paypalOptions;

        public PaymentService(IOptions<PaypalOptions> paypalOptions)
        {
            _paypalOptions = paypalOptions.Value;
        }

        public  HttpClient GetPaypalHttpClient()
        {
            const string sandbox = "https://api-m.sandbox.paypal.com";

            var http = new HttpClient
            {
                BaseAddress = new Uri(sandbox),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

        public async Task<PayPalAccessToken> GetPayPalAccessTokenAsync(HttpClient http)
        {

            byte[] bytes = Encoding.GetEncoding("iso-8859-1").GetBytes($"{_paypalOptions.PayPalClientId}:{_paypalOptions.PayPalClientSecret}");

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            var form = new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials"
            };

            request.Content = new FormUrlEncodedContent(form);

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalAccessToken accessToken = JsonConvert.DeserializeObject<PayPalAccessToken>(content);
            return accessToken;
        }

        private async Task<PayPalPaymentCreatedResponse> CreatePaypalPaymentAsync(HttpClient http, PayPalAccessToken accessToken)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "v1/payments/payment");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                intent = "sale",
                redirect_urls = new
                {
                    return_url = "http://google.com",
                    cancel_url = "http://google.com"
                },
                payer = new { payment_method = "paypal" },
                transactions = JArray.FromObject(new[]
                {
            new
            {
                amount = new
                {
                    total = 7.47,
                    currency = "USD"
                }
            }
        })
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);

            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentCreatedResponse paypalPaymentCreated = JsonConvert.DeserializeObject<PayPalPaymentCreatedResponse>(content);
            return paypalPaymentCreated;
        }

        private async Task<PayPalPaymentExecutedResponse> ExecutePaypalPaymentAsync(HttpClient http, PayPalAccessToken accessToken, string paymentId, string payerId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v1/payments/payment/{paymentId}/execute");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.access_token);

            var payment = JObject.FromObject(new
            {
                payer_id = payerId
            });

            request.Content = new StringContent(JsonConvert.SerializeObject(payment), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await http.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PayPalPaymentExecutedResponse executedPayment = JsonConvert.DeserializeObject<PayPalPaymentExecutedResponse>(content);
            return executedPayment;
        }
    }
}
