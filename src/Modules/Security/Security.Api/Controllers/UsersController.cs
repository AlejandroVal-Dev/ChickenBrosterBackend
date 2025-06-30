using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.DTOs.User;
using Security.Application.Services;
using Security.Application.UseCases;
using SharedKernel.Util;

namespace Security.Api.Controllers
{
    [Route("security/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _userService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByUsername([FromQuery] string username)
        {
            var result = await _userService.SearchByUsernameAsync(username);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto request)
        {
            var result = await _userService.RegisterUserAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdUser = await _userService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdUser.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserDto request)
        {
            var result = await _userService.UpdateUserAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _userService.DeleteUserAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _userService.RestoreUserAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
