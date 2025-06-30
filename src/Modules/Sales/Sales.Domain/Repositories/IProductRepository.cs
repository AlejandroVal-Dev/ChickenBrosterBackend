using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface IProductRepository
    {
        // Reading
        Task<IReadOnlyList<Product>> GetAllAsync();
        Task<IReadOnlyList<Product>> GetActivesAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetByCategoryAsync(int categoryId);

        // Searching
        Task<IReadOnlyList<Product>> SearchByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeactivateAsync(Product product);
        Task RestoreAsync(Product product);

        // From category assign
        Task AssignCategoryAsync(ProductCategoryAssignment assignment);
        Task UnassignCategoryAsync(ProductCategoryAssignment assignment);
        Task<IReadOnlyList<int>> GetAssignedCategoryIdsAsync(int productId);

    }
}
