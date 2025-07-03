using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.ProductCategory;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productCategoryService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _productCategoryService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productCategoryService.GetByIdAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _productCategoryService.SearchByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCategoryDto request)
        {
            var result = await _productCategoryService.CreateCategoryAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _productCategoryService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCategoryDto dto)
        {
            var result = await _productCategoryService.UpdateCategoryAsync(dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productCategoryService.DeleteCategoryAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _productCategoryService.RestoreCategoryAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpGet("{productId}/assigned-categories")]
        public async Task<IActionResult> GetAssignedCategories(int productId)
        {
            var result = await _productCategoryService.GetAssignedByProduct(productId);

            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
