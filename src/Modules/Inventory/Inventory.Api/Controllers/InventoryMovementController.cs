using Inventory.Application.DTOs.InventoryMovement;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    [Authorize]
    public class InventoryMovementController : ControllerBase
    {
        private readonly IInventoryMovementService _inventoryMovementService;

        public InventoryMovementController(IInventoryMovementService inventoryMovementService)
        {
            _inventoryMovementService = inventoryMovementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _inventoryMovementService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _inventoryMovementService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("filtered")]
        public async Task<IActionResult> GetByFilters(InventoryMovementFilterDto filter)
        {
            var result = await _inventoryMovementService.GetFilteredAsync(filter);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInventoryMovementDto request)
        {
            var result = await _inventoryMovementService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _inventoryMovementService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }
    }
}
