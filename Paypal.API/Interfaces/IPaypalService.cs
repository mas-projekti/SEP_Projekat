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
        Task<PayPalPaymentCreatedResponse> CreatePaypalPaymentAsync(PayPalAccessToken accessToken);
        Task<PayPalPaymentExecutedResponse> ExecutePaypalPaymentAsync(PayPalAccessToken accessToken, string paymentId, string payerId);
    }
}
