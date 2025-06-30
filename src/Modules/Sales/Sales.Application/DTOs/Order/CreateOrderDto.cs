using Sales.Application.DTOs.OrderItem;
using Sales.Domain.Enums;

namespace Sales.Application.DTOs.Order
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int? TableId { get; set; }
        public OrderType OrderType { get; set; }
        public List<AddOrderItemDto> Items { get; set; } = new();
    }
}
