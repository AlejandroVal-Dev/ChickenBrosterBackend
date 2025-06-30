using Inventory.Application.DTOs.UnitOfMeasure;
using Inventory.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers
{
    [Route("inventory/[controller]")]
    [ApiController]
    [Authorize]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly IUnitOfMeasureService _unitOfMeasureService;

        public UnitOfMeasureController(IUnitOfMeasureService unitOfMeasureService)
        {
            _unitOfMeasureService = unitOfMeasureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfMeasureService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _unitOfMeasureService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _unitOfMeasureService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByName([FromQuery] string name)
        {
            var result = await _unitOfMeasureService.SearchByNameAsync(name);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUnitOfMeasureDto request)
        {
            var result = await _unitOfMeasureService.CreateAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var createdUnit = await _unitOfMeasureService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUnitOfMeasureDto request)
        {
            var result = await _unitOfMeasureService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _unitOfMeasureService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _unitOfMeasureService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

    }
}
