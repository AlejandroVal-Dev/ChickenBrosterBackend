using Inventory.Application.Util;
using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Repository;

namespace Inventory.Infrastructure.Persistence
{
    public class InventoryUnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _database;

        public IIngredientRepository Ingredients { get; }

        public IInventoryRepository Inventories { get; }

        public IRecipeRepository Recipes { get; }

        public IUnitOfMeasureRepository UnitsOfMeasure { get; }

        public IIngredientCategoryRepository Categories { get; }

        public IInventoryMovementRepository InventoryMovements { get; }

        public InventoryUnitOfWork(InventoryDbContext database)
        {
            _database = database;
            Ingredients = new IngredientRepository(database);
            Inventories = new InventoryRepository(database);
            Recipes = new RecipeRepository(database);
            UnitsOfMeasure = new UnitOfMeasureRepository(database);
            Categories = new IngredientCategoryRepository(database);
            InventoryMovements = new InventoryMovementRepository(database);
        }

        public Task<int> CommitAsync() => _database.SaveChangesAsync();
    }
}
