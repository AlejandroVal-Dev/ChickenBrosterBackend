using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.DTOs.Login;
using Security.Application.UseCases;

namespace Security.Api.Controllers
{
    [ApiController]
    [Route("security/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request);

            if (result.IsFailure)
                return Unauthorized(new { error = result.Error });

            return Ok(result.Value);
        }
    }
}
