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
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("user/{userId}/orders")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserOrders(int userId)
        {
            try
            {
                IEnumerable<OrderDto> orders = await _orderService.GetUserOrders(userId);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                OrderDto order = await _orderService.GetOrderById(id);
                return Ok(order);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOrder([FromBody] InputOrderDto inputOrder)
        {
            try
            {
                OrderDto insertedOrder = await _orderService.InsertOrder(inputOrder);
                return CreatedAtAction(nameof(GetById), new { id = insertedOrder.Id }, insertedOrder);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

 

    }
}
