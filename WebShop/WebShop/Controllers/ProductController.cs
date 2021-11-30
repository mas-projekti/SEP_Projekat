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
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDto>))]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _productService.GetAllProducts());

        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                ProductDto product = await _productService.GetProductById(id);
                return Ok(product);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto product)
        {
            try
            {
                ProductDto insertedProduct = await _productService.InsertProduct(product);
                return CreatedAtAction(nameof(CreateProduct), new { id = insertedProduct.Id }, insertedProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EditProduct([FromBody] ProductDto product)
        {
            try
            {
                ProductDto modified = await _productService.UpdateProduct(product);
                return Ok(modified);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            try
            {
                await _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
