using Microsoft.EntityFrameworkCore;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;
using SharedKernel.Entities;

namespace Security.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly SecurityDbContext _database;

        public PersonRepository(SecurityDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<Person>> GetAllAsync()
        {
            return await _database.People
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Person>> GetActivesAsync()
        {
            return await _database.People
                .Where (p => p.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(int id)
        {
            return await _database.People
                .FindAsync(id);
        }

        public async Task<Person?> GetByEmailAsync(string email)
        {
            return await _database.People
                .AsNoTracking()
                .FirstOrDefaultAsync(p  => p.Email == email);
        }

        public async Task<IReadOnlyList<Person>> SearchByNameAsync(string name)
        {
            return await _database.People
                .Where(p => p.IsActive && EF.Functions.ILike(p.Name, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Person>> SearchByPhoneAsync(string phoneNumber)
        {
#pragma warning disable CS8604 
            return await _database.People
                .Where(p => EF.Functions.ILike(p.PhoneNumber, $"%{phoneNumber}%"))
                .ToListAsync();
#pragma warning restore CS8604 
        }

        public async Task<bool> ExistsByDocumentAsync(string documentId)
        {
            return await _database.People
                .AnyAsync(p => p.DocumentId == documentId);
        }

        public async Task AddAsync(Person person)
        {
            await _database.People
                .AddAsync(person);
        }

        public Task UpdateAsync(Person person)
        {
            _database.People.Update(person);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(Person person)
        {
            _database.People.Update(person);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(Person person)
        {
            _database.People.Update(person);
            return Task.CompletedTask;
        }
    }
}
