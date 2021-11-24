using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paypal.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Controllers
{
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaypalService _paypalService;

        public PaymentController(IPaypalService paymentService)
        {
            _paypalService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> Test()
        {
            var token = await _paypalService.GetPayPalAccessTokenAsync();
            var createdPayment = await _paypalService.CreatePaypalPaymentAsync(token);
            //var executedPayment = await _paypalService.ExecutePaypalPaymentAsync(token, createdPayment.id, "dmsad0a");
            return Ok(createdPayment);
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var token = await _paypalService.GetPayPalAccessTokenAsync();
            return Ok(token);
        }

    }
}
