namespace Sales.Application.DTOs.OrderItem
{
    public class UpdateOrderItemDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
