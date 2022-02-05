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
    [Route("api/clients")]
    [ApiController]
    public class BankClientController : ControllerBase
    {

        private readonly IBankClientService _bankService;

        public BankClientController(IBankClientService bankService)
        {
            _bankService = bankService;
        }


        [HttpPost]
        public IActionResult CreateUser([FromBody] ClientRegisterDto dto)
        {

            return Ok(_bankService.RegisterNewClient(dto));
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {

            return Ok(_bankService.Login(dto));
        }

    }
}
