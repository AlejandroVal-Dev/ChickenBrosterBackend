using Security.Application.DTOs.Login;
using SharedKernel.Util;

namespace Security.Application.UseCases
{
    public interface IAuthService
    {
        Task<Result<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    }
}
