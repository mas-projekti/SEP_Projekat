﻿
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paypal.API.Dto;
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

        public PaypalService(IOptions<PaypalOptions> paypalOptions, IDataAdapter dataAdapter)
        {
            _paypalOptions = paypalOptions.Value;
            paypalClient = GetPaypalHttpClient();
            this.dataAdapter = dataAdapter;

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
            PayPalAccessToken token = await GetPayPalAccessTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/checkout/orders");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);

            JObject orderObj = dataAdapter.RepackOrderToJObject(order);

            request.Content = new StringContent(JsonConvert.SerializeObject(orderObj), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await paypalClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PaypalOrderCreatedResponse createdOrder = JsonConvert.DeserializeObject<PaypalOrderCreatedResponse>(content);
            return createdOrder;
        }

        public async Task<PaypalOrderCapturedResponse> CapturePaypalOrderAsync(string orderId)
        {
            PayPalAccessToken token = await GetPayPalAccessTokenAsync();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"v2/checkout/orders/{orderId}/capture");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            request.Content = new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await paypalClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();
            PaypalOrderCapturedResponse createdOrder = JsonConvert.DeserializeObject<PaypalOrderCapturedResponse>(content);
            return createdOrder;

        }
    }
}
