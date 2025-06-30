using Cashbox.Application.DTOs.CashRegisterMovement;
using Cashbox.Application.DTOs.CashRegisterSession;
using Cashbox.Application.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cashbox.Api.Controllers
{
    [Route("cashbox/[controller]")]
    [ApiController]
    [Authorize]
    public class CashRegisterController : ControllerBase
    {
        private readonly ICashRegisterService _cashRegisterService;

        public CashRegisterController(ICashRegisterService cashRegisterService)
        {
            _cashRegisterService = cashRegisterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cashRegisterService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetSessionById(int sessionId)
        {
            var result = await _cashRegisterService.GetSessionByIdAsync(sessionId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("by-date-range")]
        public async Task<IActionResult> GetSessionsByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            var result = await _cashRegisterService.GetSessionsByDateRangeAsync(from, to);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("open")]
        public async Task<IActionResult> OpenSession([FromBody] OpenCashRegisterSessionDto request)
        {
            var result = await _cashRegisterService.OpenSessionAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { SessionId = result.Value });
        }

        [HttpPost("close")]
        public async Task<IActionResult> CloseSession([FromBody] CloseCashRegisterSessionDto request)
        {
            var result = await _cashRegisterService.CloseSessionAsync(request);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpGet("{sessionId}/movements")]
        public async Task<IActionResult> GetMovementsBySessionId(int sessionId)
        {
            var result = await _cashRegisterService.GetMovementsBySessionIdAsync(sessionId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("movement")]
        public async Task<IActionResult> AddMovement([FromBody] CreateCashRegisterMovementDto request)
        {
            var result = await _cashRegisterService.AddMovementAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(new { MovementId = result.Value });
        }

        [HttpGet("{sessionId}/orders")]
        public async Task<IActionResult> GetOrdersBySessionId(int sessionId)
        {
            var result = await _cashRegisterService.GetOrdersBySessionIdAsync(sessionId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
