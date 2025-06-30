namespace Inventory.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; private set; }

        // Core properties
        public int IngredientId { get; private set; }
        public decimal ActualStock { get; private set; }
        public decimal? MinimumStock { get; private set; }
        public DateTime? LastMovement {  get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public Inventory(int ingredientId, decimal? actualStock, decimal? minimumStock)
        {
            IngredientId = ingredientId;
            ActualStock = actualStock ?? 0m;
            MinimumStock = minimumStock;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Inventory() { }

        // Public methods
        public void UpdateMinimumStock(decimal? minimumStock) 
        {
            MinimumStock = minimumStock;
            UpdatedAt = DateTime.UtcNow;
        }

        public bool UnderMinimum()
        {
            if (MinimumStock.HasValue)
                return ActualStock < MinimumStock;

            return false;
        }

        public void Increase(decimal quantity)
        {
            ActualStock += quantity;
            LastMovement = DateTime.UtcNow;
        }

        public void Decrease(decimal quantity)
        {
            ActualStock -= quantity;
            LastMovement = DateTime.UtcNow;
        }

        public void Fit(decimal quantity)
        {
            ActualStock = quantity;
            LastMovement = DateTime.UtcNow;
        }
    }
}
