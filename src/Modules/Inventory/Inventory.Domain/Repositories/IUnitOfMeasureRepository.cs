using Inventory.Domain.Entities;

namespace Inventory.Domain.Repositories
{
    public interface IUnitOfMeasureRepository
    {
        // Reading
        Task<IReadOnlyList<UnitOfMeasure>> GetAllAsync();
        Task<IReadOnlyList<UnitOfMeasure>> GetActivesAsync();
        Task<UnitOfMeasure?> GetByIdAsync(int id);

        // Searching
        Task<IReadOnlyList<UnitOfMeasure>> SearchByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(UnitOfMeasure unitOfMeasure);
        Task UpdateAsync(UnitOfMeasure unitOfMeasure);
        Task DeactivateAsync(UnitOfMeasure unitOfMeasure);
        Task RestoreAsync(UnitOfMeasure unitOfMeasure);
    }
}
