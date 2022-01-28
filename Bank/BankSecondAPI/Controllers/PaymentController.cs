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
        public async Task<IActionResult> Post([FromBody] PayWithCardDto payWithCard)
        {
            return Ok(await _paymentService.AuthorizePayment(payWithCard));

        }

        [HttpPost("external")]
        public async Task<IActionResult> ProcessExternal([FromBody] TransBankPaymentRequestDto request)
        {
            return Ok(await _paymentService.ProcessExternalPayment(request));

        }

        [HttpGet("{paymentId}")]
        public async Task<IActionResult> ProcessExternal(int paymentId)
        {
            return Ok(await _paymentService.GetTransactionByPaymentId(paymentId));

        }

    }
}
