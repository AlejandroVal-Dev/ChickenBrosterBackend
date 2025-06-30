using Inventory.Domain.Repositories;

namespace Inventory.Application.Util
{
    public interface IUnitOfWork
    {
        IIngredientRepository Ingredients { get; }
        IInventoryRepository Inventories { get; }
        IRecipeRepository Recipes { get; }
        IUnitOfMeasureRepository UnitsOfMeasure { get; }
        IIngredientCategoryRepository Categories {  get; }
        IInventoryMovementRepository InventoryMovements { get; }
        Task<int> CommitAsync();
    }
}
