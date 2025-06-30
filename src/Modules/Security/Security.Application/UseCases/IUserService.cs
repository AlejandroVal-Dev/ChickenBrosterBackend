using Security.Application.DTOs.User;
using SharedKernel.Util;

namespace Security.Application.UseCases
{
    public interface IUserService
    {
        Task<Result<IReadOnlyList<UserDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<UserDto>>> GetActivesAsync();
        Task<Result<UserDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<UserDto>>> SearchByUsernameAsync(string username);
        Task<Result<int>> RegisterUserAsync(CreateUserDto request);
        Task<Result> UpdateUserAsync(UpdateUserDto request);
        Task<Result> ChangePasswordAsync(int userId, string newPassword);
        Task<Result> DeleteUserAsync(int userId);
        Task<Result> RestoreUserAsync(int userId);
        
    }
}
