using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Ingredient> Ingredients => Set<Ingredient>();
        public DbSet<Domain.Entities.Inventory> Inventories => Set<Domain.Entities.Inventory>();
        public DbSet<IngredientCategory> Categories => Set<IngredientCategory>();
        public DbSet<Recipe> Recipes => Set<Recipe>();
        public DbSet<UnitOfMeasure> UnitsOfMeasure => Set<UnitOfMeasure>();
        public DbSet<IngredientCategoryAssigment> IngredientCategoryAssignments => Set<IngredientCategoryAssigment>();
        public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();
        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
        }
    }
}
