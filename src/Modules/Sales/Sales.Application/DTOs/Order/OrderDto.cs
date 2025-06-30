using Sales.Application.DTOs.OrderItem;
using Sales.Domain.Enums;

namespace Sales.Application.DTOs.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? TableId { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
