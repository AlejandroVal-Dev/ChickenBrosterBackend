using Security.Domain.Entities;

namespace Security.Domain.Repositories
{
    public interface IPermissionRepository
    {
        // Reading
        Task<IReadOnlyList<Permission>> GetAllAsync();
        Task<IReadOnlyList<Permission>> GetActivesAsync();
        Task<Permission?> GetByIdAsync(int id);
        Task<Permission?> GetByCodeAsync(string code);

        // Searching
        Task<IReadOnlyList<Permission>> SearchByCodeAsync(string code);

        // Validation
        Task<bool> ExistsByCodeAsync(string code);

        // Writting
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeactivateAsync(Permission permission);
        Task RestoreAsync(Permission permission);
    }
}
