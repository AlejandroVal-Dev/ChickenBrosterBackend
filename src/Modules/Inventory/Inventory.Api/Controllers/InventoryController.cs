using Inventory.Application.DTOs.Inventory;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inventoryService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _inventoryService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIngredientId(int id)
        {
            var result = await _inventoryService.GetByIngredientIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var result = await _inventoryService.GetLowStockAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateInventoryMinimumStockDto request)
        {
            var result = await _inventoryService.UpdateMinimumStockAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
