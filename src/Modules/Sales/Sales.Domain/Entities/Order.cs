using Sales.Domain.Enums;

namespace Sales.Domain.Entities
{
    public class Order
    {
        public int Id { get; private set; }

        // Core properties
        public int? UserId { get; private set; }
        public int? TableId { get; private set; }
        public OrderType OrderType { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }

        // Audit properties
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Order(int? userId, int? tableId, OrderType orderType)
        {
            UserId = userId;
            TableId = tableId;
            OrderType = orderType;
            Status = OrderStatus.Created;
            TotalAmount = 0m;
            CreatedAt = DateTime.UtcNow;
        }

        private Order() { }

        // Public methods
        public void UpdateTotalAmount(decimal total)
        {
            TotalAmount = total;
        }

        public void MarkAsPaid()
        {
            Status = OrderStatus.Paid;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            Status = OrderStatus.Cancelled;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
