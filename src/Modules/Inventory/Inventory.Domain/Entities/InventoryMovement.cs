using Inventory.Domain.Enums;

namespace Inventory.Domain.Entities
{
    public class InventoryMovement
    {
        public int Id { get; private set; }

        // Core properties
        public int IngredientId { get; private set; }
        public int UnitOfMeasureId { get; private set; }
        public decimal Quantity { get; private set; }
        public MovementType MovementType { get; private set; }
        public string? Reason { get; private set; }
        public int? MadeByUserId { get; private set; }
        public DateTime MovementDate { get; private set; }

        // Constructors
        public InventoryMovement(int ingredientId, int unitOfMeasureId, decimal quantity, MovementType movementType, string? reason, int? madeByUserId)
        {
            IngredientId = ingredientId;
            UnitOfMeasureId = unitOfMeasureId;
            Quantity = quantity;
            MovementType = movementType;
            Reason = reason;
            MadeByUserId = madeByUserId;
            MovementDate = DateTime.UtcNow;
        }

        private InventoryMovement() { }

        // Public methods
        public bool IsValid()
        {
            return Quantity > 0;
        }

        public string Summary()
        {
            return $"{MovementType}: {Quantity} {UnitOfMeasureId} de {IngredientId}";
        }
    }
}
