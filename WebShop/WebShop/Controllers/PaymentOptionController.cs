using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;

namespace WebShop.Controllers
{
    [Route("api/payment-options")]
    [ApiController]
    public class PaymentOptionController : ControllerBase
    {
        private readonly IPaymentOptionService _paymentOptionService;

        public PaymentOptionController(IPaymentOptionService paymentOptionService)
        {
            _paymentOptionService = paymentOptionService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaymentOptionDto>))]
        public async Task<IActionResult> GetSupportedPaymentOptions()
        {
            return Ok(await _paymentOptionService.GetAllSuportedPaymentOptions());

        }
   
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentOptionDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSupportedPaymentOptions([FromBody] Dictionary<string, bool> paymentOptions)
        {
            try
            {
                await _paymentOptionService.UpdateSupportedPaymentOptions(paymentOptions);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
