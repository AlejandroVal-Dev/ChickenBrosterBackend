using Inventory.Application.DTOs.Ingredient;
using Inventory.Application.DTOs.IngredientCategoryAssignment;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ingredientService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _ingredientService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ingredientService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var result = await _ingredientService.GetByCategoryIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _ingredientService.SearchByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateIngredientDto request)
        {
            var result = await _ingredientService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdIngredient = await _ingredientService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdIngredient.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateIngredientDto request)
        {
            var result = await _ingredientService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _ingredientService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _ingredientService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("assign-category")]
        public async Task<IActionResult> AssignCategory([FromBody] IngredientCategoryAssignmentDto request)
        {
            var result = await _ingredientService.AssignCategoryAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("unassign-category")]
        public async Task<IActionResult> UnassignCategory([FromBody] IngredientCategoryAssignmentDto request)
        {
            var result = await _ingredientService.UnassignCategoryAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpGet("throw")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception");
        }
    }
}
