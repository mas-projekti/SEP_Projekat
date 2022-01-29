using Microsoft.AspNetCore.Mvc;
using PaymentCardCenter.Api.Dto;
using PaymentCardCenter.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PaymentCardCenter.Api.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PanController : ControllerBase
    {
        private readonly IPanService _panService;

        public PanController(IPanService panService)
        {
            _panService = panService;
        }

        // POST api/<PanController>
        [HttpPost]
        public async Task<IActionResult> RedirectPayment([FromBody] TransBankPaymentRequestDto request)
        {
            try
            {

                return Ok(await _panService.RedirectPaymentToIssuer(request));
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
