using SharedKernel.Entities;

namespace Security.Domain.Repositories
{
    public interface IPersonRepository
    {
        // Reading
        Task<IReadOnlyList<Person>> GetAllAsync();
        Task<IReadOnlyList<Person>> GetActivesAsync();
        Task<Person?> GetByIdAsync(int id);
        Task<Person?> GetByEmailAsync(string email);

        // Searching
        Task<IReadOnlyList<Person>> SearchByNameAsync(string name);
        Task<IReadOnlyList<Person>> SearchByPhoneAsync(string phoneNumber);

        // Validation
        Task<bool> ExistsByDocumentAsync(string documentId);

        // Writting
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeactivateAsync(Person person);
        Task RestoreAsync(Person person);
    }
}
