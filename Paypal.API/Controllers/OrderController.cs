using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paypal.API.Dto;
using Paypal.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Controllers
{
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IPaypalService _paypalService;

        public OrderController(IPaypalService paymentService)
        {
            _paypalService = paymentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var createdOrder = await _paypalService.CreatePaypalOrderAsync(dto);
            return Ok(createdOrder);
        }

        [HttpPost("{id}/capture")]
        public async Task<IActionResult> CaptureOrder(string id)
        {
            var capturedOrder = await _paypalService.CapturePaypalOrderAsync(id);
            return Ok(capturedOrder);
        }


        [HttpGet("all")]
        public async Task<IActionResult> Get()
        {
            var token = await _paypalService.GetPayPalAccessTokenAsync();
            return Ok(token);
        }

    }
}
