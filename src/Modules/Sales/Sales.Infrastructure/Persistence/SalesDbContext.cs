using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;

namespace Sales.Infrastructure.Persistence
{
    public class SalesDbContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<ProductCategory> Categories => Set<ProductCategory>();
        public DbSet<Table> Tables => Set<Table>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<ProductRecipe> ProductRecipes => Set<ProductRecipe>();
        public DbSet<ProductCategoryAssignment> ProductCategoryAssignments => Set<ProductCategoryAssignment>();

        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);
        }

    }
}
