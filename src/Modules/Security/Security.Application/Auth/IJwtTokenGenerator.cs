using Security.Domain.Entities;

namespace Security.Application.Auth
{
    public interface IJwtTokenGenerator
    {
        JwtTokenResult GenerateToken(User user);
    }
}
