using BTC.Api.Dtos;
using BTC.Api.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BTC.Api.Services
{
    public class DataConverter : IDataConverter
    {
        private const string _blockchain = "https://blockchain.info";
        private HttpClient blockchainHttpClient;

        public DataConverter()
        {
            blockchainHttpClient = GetBlockchainHttpClient();
        }


        public async Task<JObject> RepackOrderToJObjectAsync(BitcoinOrderDto dto)
        {
            if (dto.OrderItems.Count == 0)
                throw new Exception("Cannot place order with zero items.");


            double totalAmount = CalculateTotalAmount(dto.OrderItems);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"tobtc?currency=USD&value={totalAmount}");


            HttpResponseMessage response = await blockchainHttpClient.SendAsync(request);
            string content = await response.Content.ReadAsStringAsync();

            double amountBTC = Double.Parse(content);
            string description = GetDescription(dto.OrderItems);

            //Create order form purchase units
            JObject order = JObject.FromObject(
            new
            {
                order_id = String.Format("{0} - {1}", dto.TransactionId, dto.MerchantId),
                price_amount = amountBTC,
                price_currency = "BTC",
                receive_currency = "BTC",
                title = "New purchase",
                description = description,
                purchaser_email = dto.Email,
                success_url = dto.SuccessUrl,
                cancel_url = dto.CancelUrl

            });
            return order;
        }

        private double CalculateTotalAmount(List<OrderItemDto> orderItems)
        {
            double totalAmount = 0;

            foreach(OrderItemDto orderItem in orderItems)
            {
                totalAmount += orderItem.Quantity * orderItem.Value;
            }

            return totalAmount;
        }

        private string GetDescription(List<OrderItemDto> orderItems)
        {
            string description = "Order items: " + Environment.NewLine + Environment.NewLine;

            foreach (OrderItemDto orderItem in orderItems)
            {
                description += $"{orderItem.Quantity} x {orderItem.Name};" + Environment.NewLine;
            }

            return description;
        }

        private HttpClient GetBlockchainHttpClient()
        {

            var http = new HttpClient
            {
                BaseAddress = new Uri(_blockchain),
                Timeout = TimeSpan.FromSeconds(30),
            };

            return http;
        }

    }
}
