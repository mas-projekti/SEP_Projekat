using BankApi.Dto;
using BankApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankApi.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentRequestDto request)
        {
            return Ok(_paymentService.CreatePaymentTransaction(request));

        }

        [HttpPost("pay")]
        public IActionResult Post([FromBody] PayWithCardDto payWithCard)
        {
            return Ok(_paymentService.AuthorizePayment(payWithCard));

        }

    }
}
