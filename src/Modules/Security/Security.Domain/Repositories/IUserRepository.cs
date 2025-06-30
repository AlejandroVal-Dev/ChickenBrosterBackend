using Security.Domain.Entities;

namespace Security.Domain.Repositories
{
    public interface IUserRepository
    {
        // Reading
        Task<IReadOnlyList<User>> GetAllAsync();
        Task<IReadOnlyList<User>> GetAllActivesAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);

        // Searching
        Task<IReadOnlyList<User>> SearchByUsernameAsync(string username);

        // Validation
        Task<bool> ExistsByUsernameAsync(string username);

        // Writting
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeactivateAsync(User user);
        Task RestoreAsync(User user);
    }
}
