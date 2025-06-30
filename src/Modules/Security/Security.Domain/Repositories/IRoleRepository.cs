using Security.Domain.Entities;

namespace Security.Domain.Repositories
{
    public interface IRoleRepository
    {
        // Reading
        Task<IReadOnlyList<Role>> GetAllAsync();
        Task<IReadOnlyList<Role>> GetActivesAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<Role?> GetByNameAsync(string name);

        // Validation
        Task<bool> ExistsByNameAsync(string name);

        // Writting
        Task AddAsync(Role role);
        Task UpdateAsync(Role role);
        Task DeactivateAsync(Role role);
        Task RestoreAsync(Role role);
    }
}
