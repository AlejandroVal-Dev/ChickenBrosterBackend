using Security.Application.DTOs.User;
using Security.Application.UseCases;
using Security.Application.Util;
using Security.Domain.Entities;
using SharedKernel.Entities;
using SharedKernel.Enums;
using SharedKernel.Util;

namespace Security.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<UserDto>>> GetAllAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var person = await _unitOfWork.People.GetByIdAsync(user.PersonId);
                if (person is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Person not found.");

                var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
                if (role is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Role not found.");

                result.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = person.GetFullName(),
                    RoleName = role.Name ?? "(Rol eliminado)",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return Result<IReadOnlyList<UserDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<UserDto>>> GetActivesAsync()
        {
            var users = await _unitOfWork.Users.GetAllActivesAsync();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var person = await _unitOfWork.People.GetByIdAsync(user.PersonId);
                if (person is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Person not found.");

                var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
                if (role is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Role not found.");

                result.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = person.GetFullName(),
                    RoleName = role.Name ?? "(Rol eliminado)",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return Result<IReadOnlyList<UserDto>>.Success(result);
        }

        public async Task<Result<UserDto>> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user is null)
                return Result<UserDto>.Failure("User not found.");

            var person = await _unitOfWork.People.GetByIdAsync(user.PersonId);
            if (person is null)
                return Result<UserDto>.Failure("Person not found.");

            var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
            if (role is null)
                return Result<UserDto>.Failure("Role not found.");

            var dto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = person.GetFullName(),
                RoleName = role.Name ?? "(Rol eliminado)",
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Result<UserDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<UserDto>>> SearchByUsernameAsync(string username)
        {
            var users = await _unitOfWork.Users.SearchByUsernameAsync(username);
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var person = await _unitOfWork.People.GetByIdAsync(user.PersonId);
                if (person is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Person not found.");

                var role = await _unitOfWork.Roles.GetByIdAsync(user.RoleId);
                if (role is null)
                    return Result<IReadOnlyList<UserDto>>.Failure("Role not found.");

                result.Add(new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    FullName = person.GetFullName(),
                    RoleName = role.Name ?? "(Rol eliminado)",
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt
                });
            }

            return Result<IReadOnlyList<UserDto>>.Success(result);
        }

        public async Task<Result<int>> RegisterUserAsync(CreateUserDto request)
        {
            if (await _unitOfWork.Users.ExistsByUsernameAsync(request.Username))
                return Result<int>.Failure("Username already exists.");

            if (await _unitOfWork.People.ExistsByDocumentAsync(request.DocumentId))
                return Result<int>.Failure("Person with same document already exists.");

            var person = new Person(
                request.Name,
                request.LastName1,
                request.LastName2,
                request.DocumentId,
                request.DocumentType,
                request.PhoneNumber,
                request.Email,
                PersonType.Employee 
            );

            await _unitOfWork.People.AddAsync(person);
            await _unitOfWork.CommitAsync();

            var role = await _unitOfWork.Roles.GetByIdAsync(request.RoleId) ?? throw new Exception("Role not found.");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User(
                request.Username,
                hashedPassword,
                person.Id,
                request.RoleId
            );

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CommitAsync();

            var result = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                FullName = person.GetFullName(),
                RoleName = role.Name,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };

            return Result<int>.Success(result.Id);
        }

        public async Task<Result> UpdateUserAsync(UpdateUserDto request)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure("User not found.");

            user.UpdateProfile(request.NewUsername, request.NewRoleId);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> ChangePasswordAsync(int userId, string newPassword)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user is null)
                return Result.Failure("User not found.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

            user.ChangePassword(hashedPassword);
            await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user is null)
                return Result.Failure("User not found.");

            user.Delete();
            await _unitOfWork.Users.DeactivateAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreUserAsync(int userId)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user is null)
                return Result.Failure("User not found.");

            user.Restore();
            await _unitOfWork.Users.RestoreAsync(user);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

    }
}
