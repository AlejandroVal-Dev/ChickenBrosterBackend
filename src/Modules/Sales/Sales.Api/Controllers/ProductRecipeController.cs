using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.ProductRecipe;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductRecipeController : ControllerBase
    {
        private readonly IProductRecipeService _productRecipeService;

        public ProductRecipeController(IProductRecipeService productRecipeService)
        {
            _productRecipeService = productRecipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productRecipeService.GetAllAsync();
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByProductId(int productId)
        {
            var result = await _productRecipeService.GetByProductIdAsync(productId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRecipeDto request)
        {
            var result = await _productRecipeService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductRecipeDto dto)
        {
            var result = await _productRecipeService.UpdateQuantityAsync(dto);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{productRecipeId}/deactivate")]
        public async Task<IActionResult> Delete(int productRecipeId)
        {
            var result = await _productRecipeService.DeleteRecipeAsync(productRecipeId);
            
            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }

        [HttpPost("{productRecipeId}/restore")]
        public async Task<IActionResult> Restore(int productRecipeId)
        {
            var result = await _productRecipeService.RestoreRecipeAsync(productRecipeId);
            
            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
