using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dto.Product;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IProductRepository _productRepository;

        public ProductController(ApplicationDBContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }
        [HttpGet]
        [Authorize]

        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {

            var products = await _productRepository.GetAllSync(query);
            var ProductDto = products.Select(s => s.ToProductDto()).ToList();
            return Ok(ProductDto);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {

            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound(); // Return a 404 if not found
            }

            return Ok(product.ToProductDto());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateProductRequestDto ProductDto)
        {
            var productModel = ProductDto.ToProductFromCreateDto();

            await _productRepository.CreateAsync(productModel);
            return CreatedAtAction(nameof(GetByID), new { Id = productModel.Id }, productModel.ToProductDto());

        }
        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductRequestDto UpdateDto)
        {
            var stockModel = await _productRepository.UpdateAsync(id, UpdateDto);
            if (stockModel == null)
            {
                return NotFound();

            }

            return Ok(stockModel.ToProductDto());
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _productRepository.Delete(id);
            if (stockModel == null)
            {
                return NotFound();
            }
            return NoContent();

        }





    }
}