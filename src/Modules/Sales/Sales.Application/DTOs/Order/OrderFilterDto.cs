using Sales.Domain.Enums;

namespace Sales.Application.DTOs.Order
{
    public class OrderFilterDto
    {
        public OrderType? OrderType { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
