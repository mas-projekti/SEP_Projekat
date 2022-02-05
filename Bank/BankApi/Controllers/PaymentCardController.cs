using BankApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankApi.Controllers
{
    [Route("api/payment-cards")]
    [ApiController]
    public class PaymentCardController : ControllerBase
    {
        private readonly IPaymentCardService _paymentCardService;

        public PaymentCardController(IPaymentCardService paymentCardService)
        {
            _paymentCardService = paymentCardService;
        }

        [HttpPost("user/{userId}")]
        public IActionResult CreateCard(int userId)
        {
            //return Ok(_paymentCardService.GeneratePaymentCard(int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value)));
            return Ok(_paymentCardService.GeneratePaymentCard(userId));

        }

    }
}
