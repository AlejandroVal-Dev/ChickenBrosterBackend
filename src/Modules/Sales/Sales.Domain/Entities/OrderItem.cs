namespace Sales.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; private set; }

        // Core properties
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        // Constructors
        public OrderItem(int orderId, int productId, int quantity, decimal unitPrice)
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = UnitPrice * Quantity;
        }

        private OrderItem() { }

        // Public methods
        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
            TotalPrice = UnitPrice * Quantity;
        }
    }
}
