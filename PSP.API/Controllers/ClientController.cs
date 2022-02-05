using Microsoft.AspNetCore.Authorization;
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
    [Route("api/clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }


        [HttpGet("{clientID}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PspClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(string clientID)
        {

            return Ok(_clientService.GetClientByClientID(clientID));

        }

        [HttpGet("{id}/info")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PspClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClientAsync(int id)
        {

            return Ok(await _clientService.GetClient(id));

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatedPspClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateClient([FromBody] NewPspClientDto newClient)
        {

            CreatedPspClientDto createdDto = await _clientService.CreateClient(newClient);
            return Created("", createdDto);

        }

        [HttpPost("notify/{id}")]
        public async Task<IActionResult> NotifyTransactionSuccessful(Guid id)
        {

            await _clientService.NotifyClientTransactionFinishedAsync(id);
            return Ok();

        }


        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PspClientDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateClient(int id,[FromBody] PspClientDto newClient)
        {
            if(_clientService.CheckUsersRightsToUpdate(User, id))
                return Ok(await _clientService.UpdateClient(id,newClient));

            return Unauthorized();

        }


    }
}
