using Inventory.Domain.Entities;
using Inventory.Domain.Enums;

namespace Inventory.Domain.Repositories
{
    public interface IInventoryMovementRepository
    {
        // Reading
        Task<IReadOnlyList<InventoryMovement>> GetAllAsync();
        Task<InventoryMovement?> GetByIdAsync(int id);
        Task<IReadOnlyList<InventoryMovement>> GetFilteredAsync(int? ingredientId, DateTime? from, DateTime? to, MovementType? movementType, int? madeByUserId);

        // Writting
        Task AddAsync(InventoryMovement movement);
    }
}
