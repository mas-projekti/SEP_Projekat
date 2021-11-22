using Paypal.API.IntegrationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paypal.API.Interfaces
{
    public interface IPaymentService
    {
        HttpClient GetPaypalHttpClient();
        Task<PayPalAccessToken> GetPayPalAccessTokenAsync(HttpClient http);
    }
}
