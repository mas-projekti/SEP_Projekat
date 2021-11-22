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
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Pay()
        {
            return Ok("Platio si baki, sve je ok");
        }

        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var nesto = _paymentService.GetPaypalHttpClient();
            var token = await _paymentService.GetPayPalAccessTokenAsync(nesto);
            return Ok(token);
        }

    }
}
