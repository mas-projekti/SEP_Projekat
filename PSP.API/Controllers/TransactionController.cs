using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSP.API.Dto;
using PSP.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Controllers
{
    [Route("api/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TransactionDto))]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            TransactionDto transactionDto = await _transactionService.Get(id);
            if (transactionDto == null)
                return NotFound();

            return Ok(transactionDto);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> AddTransaction([FromBody] List<ItemDto> items)
        {
            try
            {
                TransactionDto transacitonDto = await _transactionService.Insert(items);
                return CreatedAtAction(nameof(AddTransaction), new { id = transacitonDto.Id }, transacitonDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
       

        }

    }
}
