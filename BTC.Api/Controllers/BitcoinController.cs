using BTC.Api.Dtos;
using BTC.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Controllers
{
    [Route("api/bitcoin")]
    [ApiController]
    public class BitcoinController : ControllerBase
    {
        private readonly IBitcoinService _bitcoinService;

        public BitcoinController(IBitcoinService bitcoinService)
        {
            _bitcoinService = bitcoinService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] BitcoinOrderDto dto)
        {
            var createdOrder = await _bitcoinService.CreateBitcoinOrderAsync(dto);
            return Ok(createdOrder);
        }
    }
}
