using Newtonsoft.Json.Linq;
using Paypal.API.Dto;
using Paypal.API.IntegrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paypal.API.Interfaces
{
    public interface IPaypalService
    {
        Task<PayPalAccessToken> GetPayPalAccessTokenAsync();
        Task<PaypalOrderCreatedResponse> CreatePaypalOrderAsync(CreateOrderDto order);
        Task<PaypalOrderCapturedResponse> CapturePaypalOrderAsync(string orderId);
    }
}
