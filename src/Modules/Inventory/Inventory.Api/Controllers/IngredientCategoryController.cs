using Inventory.Application.DTOs.IngredientCategory;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    [Authorize]
    public class IngredientCategoryController : ControllerBase
    {
        private readonly IIngredientCategoryService _ingredientCategoryService;

        public IngredientCategoryController(IIngredientCategoryService ingredientCategoryService)
        {
            _ingredientCategoryService = ingredientCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ingredientCategoryService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _ingredientCategoryService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ingredientCategoryService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("ingredient/{id}")]
        public async Task<IActionResult> GetByIngredientId(int id)
        {
            var result = await _ingredientCategoryService.GetByIngredientIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _ingredientCategoryService.SearchByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIngredientCategoryDto request)
        {
            var result = await _ingredientCategoryService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _ingredientCategoryService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateIngredientCategoryDto request)
        {
            var result = await _ingredientCategoryService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _ingredientCategoryService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _ingredientCategoryService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
