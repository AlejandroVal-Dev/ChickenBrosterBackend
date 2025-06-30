using Microsoft.EntityFrameworkCore;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Persistence;

namespace Sales.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly SalesDbContext _database;

        public ProductRepository(SalesDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Product>> GetAllAsync()
        {
            return await _database.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> GetActivesAsync()
        {
            return await _database.Products
                .Where(p => p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _database.Products
                .FindAsync(id);
        }

        public async Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _database.Products
                .Where(p => _database.ProductCategoryAssignments
                    .Any(pca => pca.ProductId == p.Id && pca.CategoryId == categoryId))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Product>> SearchByNameAsync(string name)
        {
            return await _database.Products
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _database.Products
                .AnyAsync(p => p.Name == name);
        }

        public async Task AddAsync(Product product)
        {
            await _database.Products
                .AddAsync(product);
        }

        public Task UpdateAsync(Product product)
        {
            _database.Products.Update(product);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Product product)
        {
            _database.Products.Update(product);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Product product)
        {
            _database.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task AssignCategoryAsync(ProductCategoryAssignment assignment)
        {
            var exists = await _database.ProductCategoryAssignments
                .AnyAsync(a => a.ProductId == assignment.ProductId && a.CategoryId == assignment.CategoryId);

            if (!exists)
            {
                var newAssignment = new ProductCategoryAssignment(assignment.ProductId, assignment.CategoryId);
                _database.ProductCategoryAssignments.Add(assignment);
                await _database.SaveChangesAsync();
            }
        }

        public async Task UnassignCategoryAsync(ProductCategoryAssignment assignment)
        {
            var newAssignment = await _database.ProductCategoryAssignments
                .FirstOrDefaultAsync(a => a.ProductId == assignment.ProductId && a.CategoryId == assignment.CategoryId);

            if (assignment != null)
            {
                _database.ProductCategoryAssignments.Remove(assignment);
                await _database.SaveChangesAsync();
            }
        }

        public async Task<IReadOnlyList<int>> GetAssignedCategoryIdsAsync(int productId)
        {
            return await _database.ProductCategoryAssignments
                .Where(a => a.ProductId == productId)
                .Select(a => a.CategoryId)
                .ToListAsync();
        }
    }
}
