using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebShop.Options;
using WebShop.Service.Contract.Dto;
using WebShop.Service.Contract.Services;
using WebShop.Service.Services;

namespace WebShop.Controllers
{
    [Route("api/payment-options")]
    [ApiController]
    public class PaymentOptionController : ControllerBase
    {
        private readonly IPaymentOptionService _paymentOptionService;
        private readonly WebhookOptions _hookOptions;

        public PaymentOptionController(IPaymentOptionService paymentOptionService, IOptions<WebhookOptions> options)
        {
            _paymentOptionService = paymentOptionService;
            _hookOptions = options.Value;
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
            SignatureVerifierService serv = new SignatureVerifierService(_hookOptions.Key);

            if (!serv.IsSignatureValid(Request.Headers["X-Sender-Signature"], JsonConvert.SerializeObject(paymentOptions)))
                return Unauthorized();

            
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
