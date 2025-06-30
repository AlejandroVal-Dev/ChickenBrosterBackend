using Microsoft.EntityFrameworkCore;
using Security.Domain.Entities;
using Security.Domain.Repositories;
using Security.Infrastructure.Persistence;

namespace Security.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SecurityDbContext _database;

        public UserRepository(SecurityDbContext database)
        {
            _database = database;
        }

        public async Task<IReadOnlyList<User>> GetAllAsync()
        {
            return await _database.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IReadOnlyList<User>> GetAllActivesAsync()
        {
            return await _database.Users
                .Where(u => u.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _database.Users
                .FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _database.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IReadOnlyList<User>> SearchByUsernameAsync(string username)
        {
            return await _database.Users
                .Where(u => u.IsActive && EF.Functions.ILike(u.Username, $"%{username}%"))
                .ToListAsync();
        }

        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            return await _database.Users
                .AnyAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _database.Users
                .AddAsync(user);
        }

        public Task UpdateAsync(User user)
        {
            _database.Users.Update(user);
            return Task.CompletedTask;
        }

        public Task DeactivateAsync(User user)
        {
            _database.Users.Update(user);
            return Task.CompletedTask;
        }

        public Task RestoreAsync(User user)
        {
            _database.Users.Update(user);
            return Task.CompletedTask;
        }
    }
}
