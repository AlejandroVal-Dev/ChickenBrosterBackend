using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.Table;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await _tableService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives() 
        {
            var result = await _tableService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tableService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByNumber([FromQuery] string number)
        {
            var result = await _tableService.SearchByNumberAsync(number);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTableDto request)
        {
            var result = await _tableService.CreateTableAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _tableService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPut("{id}/occupy")]
        public async Task<IActionResult> MarkAsOccuped(int id)
        {
            var result = await _tableService.MarkAsOccupedAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPut("{id}/available")]
        public async Task<IActionResult> MarkAsAvailable(int id)
        {
            var result = await _tableService.MarkAsAvailableAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tableService.DeleteTableAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _tableService.RestoreTableAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
