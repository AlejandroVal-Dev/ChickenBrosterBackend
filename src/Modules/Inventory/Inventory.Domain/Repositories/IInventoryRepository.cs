namespace Inventory.Domain.Repositories
{
    public interface IInventoryRepository
    {
        // Reading
        Task<IReadOnlyList<Entities.Inventory>> GetAllAsync();
        Task<IReadOnlyList<Entities.Inventory>> GetActivesAsync();
        Task<Entities.Inventory?> GetByIngredientIdAsync(int ingredientId);
        Task<IReadOnlyList<Entities.Inventory>> GetLowStockAsync();

        // Writting
        Task AddAsync(Entities.Inventory inventory);
        Task UpdateAsync(Entities.Inventory inventory);
        Task DeactivateAsync(Entities.Inventory inventory);
        Task RestoreAsync(Entities.Inventory inventory);
    }
}
