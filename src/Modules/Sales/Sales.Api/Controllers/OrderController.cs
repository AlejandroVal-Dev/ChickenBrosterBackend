using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.Application.DTOs.Order;
using Sales.Application.DTOs.OrderItem;
using Sales.Application.UseCases;

namespace Sales.Api.Controllers
{
    [Route("sales/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _orderService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetById(int orderId)
        {
            var result = await _orderService.GetByIdAsync(orderId);

            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromBody] OrderFilterDto filter)
        {
            var result = await _orderService.GetFilteredAsync(filter);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderDto request)
        {
            var result = await _orderService.CreateOrderAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            var createdUnit = await _orderService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUnit.Value);
        }

        [HttpPatch("{orderId}/mark-as-completed")]
        public async Task<IActionResult> MarkAsCompleted(int orderId)
        {
            var result = await _orderService.MarkAsCompletedAsync(orderId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPatch("{orderId}/cancel")]
        public async Task<IActionResult> Cancel(int orderId)
        {
            var result = await _orderService.CancelAsync(orderId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPost("{orderId}/items")]
        public async Task<IActionResult> AddItem(int orderId, [FromBody] AddOrderItemDto request)
        {
            var result = await _orderService.AddItemAsync(orderId, request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpPut("items")]
        public async Task<IActionResult> UpdateItemQuantity([FromBody] UpdateOrderItemDto request)
        {
            var result = await _orderService.UpdateItemQuantityAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }

        [HttpDelete("items")]
        public async Task<IActionResult> RemoveItem([FromBody] RemoveOrderItemDto request)
        {
            var result = await _orderService.RemoveItemAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();
        }
    }
}
