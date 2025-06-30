using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.Payment;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _paymentService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _paymentService.GetByIdAsync(id);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("by-order/{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var result = await _paymentService.GetByOrderIdAsync(orderId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreatePaymentDto request)
        {
            var result = await _paymentService.AddPaymentAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUnit = await _paymentService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }
    }
}
