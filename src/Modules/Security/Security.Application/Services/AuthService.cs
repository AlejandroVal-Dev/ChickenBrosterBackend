using Security.Application.Auth;
using Security.Application.DTOs.Login;
using Security.Application.UseCases;
using Security.Domain.Repositories;
using SharedKernel.Util;

namespace Security.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user is null || !user.IsActive)
                return Result<LoginResponseDto>.Failure("Invalid username or password");

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return Result<LoginResponseDto>.Failure("Invalid username or password");

            var token = _jwtTokenGenerator.GenerateToken(user);

            return Result<LoginResponseDto>.Success(new LoginResponseDto
            {
                AccessToken = token.Token,
                ExpiresAt = token.ExpiresAt,
                RoleId = user.RoleId
            });
        }
    }
}
