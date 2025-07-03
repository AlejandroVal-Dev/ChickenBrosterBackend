using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.Product;
using Sales.Application.DTOs.ProductCategoryAssignment;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _productService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _productService.GetByCategoryIdAsync(categoryId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _productService.SearchByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDto request)
        {
            var result = await _productService.CreateProductAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _productService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductDto dto)
        {
            var result = await _productService.UpdateProductAsync(dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteProductAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _productService.RestoreProductAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpPut("assign-category")]
        public async Task<IActionResult> AssignCategory([FromBody] ProductCategoryAssignmentDto dto)
        {
            var result = await _productService.AssignCategoryAsync(dto);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("unassign-category")]
        public async Task<IActionResult> UnassignCategory([FromBody] ProductCategoryAssignmentDto dto)
        {
            var result = await _productService.UnassignCategoryAsync(dto);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
