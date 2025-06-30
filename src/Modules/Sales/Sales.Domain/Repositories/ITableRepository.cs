using Sales.Domain.Entities;

namespace Sales.Domain.Repositories
{
    public interface ITableRepository
    {
        // Reading
        Task<IReadOnlyList<Table>> GetAllAsync();
        Task<IReadOnlyList<Table>> GetActivesAsync();
        Task<Table?> GetByIdAsync(int id);
        Task<IReadOnlyList<Table>> GetByAvailablityAsync(bool isAvailable
            );

        // Searching
        Task<IReadOnlyList<Table>> SearchByNumberAsync(string number);

        // Validation
        Task<bool> ExistsByNumberAsync(string number);

        // Writting
        Task AddAsync(Table table);
        Task UpdateAsync(Table table);
        Task DeactivateAsync(Table table);
        Task RestoreAsync(Table table);
    }
}
