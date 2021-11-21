using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Controllers
{
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {

        [HttpPost]
        public IActionResult Pay()
        {
            return Ok("Platio si baki, sve je ok");
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok("Sve kafane zatvorio Lola, od Ilidze pa do Marindvora.");
        }

    }
}
