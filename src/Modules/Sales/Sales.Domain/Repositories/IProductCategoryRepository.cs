using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface IProductCategoryRepository
    {
        // Reading
        Task<IReadOnlyList<ProductCategory>> GetAllAsync();
        Task<IReadOnlyList<ProductCategory>> GetActivesAsync();
        Task<ProductCategory?> GetByIdAsync(int id);
        Task<IReadOnlyList<ProductCategory>> GetChildrenAsync(int parentId);

        // Searching
        Task<IReadOnlyList<ProductCategory>> SearchByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(ProductCategory category);
        Task UpdateAsync(ProductCategory category);
        Task DeactivateAsync(ProductCategory category);
        Task RestoreAsync(ProductCategory category);
    }
}
