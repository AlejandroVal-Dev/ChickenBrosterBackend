using Inventory.Application.DTOs.Recipe;
using Inventory.Application.DTOs.RecipeIngredient;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _recipeService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _recipeService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _recipeService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _recipeService.GetByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRecipeDto request)
        {
            var result = await _recipeService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _recipeService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRecipeDto request)
        {
            var result = await _recipeService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _recipeService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _recipeService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPost("{recipeId}/ingredients")]
        public async Task<IActionResult> AddIngredient(int recipeId, [FromBody] CreateRecipeIngredientDto request)
        {
            var result = await _recipeService.AddIngredientAsync(recipeId, request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{recipeId}/ingredients/{ingredientId}")]
        public async Task<IActionResult> RemoveIngredient(int recipeId, int ingredientId)
        {
            var result = await _recipeService.RemoveIngredientAsync(recipeId, ingredientId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("{recipeId}/ingredients")]
        public async Task<IActionResult> UpdateIngredientQuantity(int recipeId, [FromBody] UpdateRecipeIngredientQuantityDto request)
        {
            var result = await _recipeService.UpdateIngredientQuantityAsync(recipeId, request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

    }
}
