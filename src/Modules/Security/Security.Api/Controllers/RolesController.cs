using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.DTOs.Role;
using Security.Application.Services;
using Security.Application.UseCases;
using SharedKernel.Util;

namespace Security.Api.Controllers
{
    [Route("security/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _roleService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{roleId}/permissions")]
        public async Task<IActionResult> GetPermissionsByRole(int roleId)
        {
            var result = await _roleService.GetPermissionsAsync(roleId);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _roleService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleDto request)
        {
            var result = await _roleService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdRole = await _roleService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdRole.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoleDto request)
        {
            var result = await _roleService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _roleService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _roleService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPost("assign-perm")]
        public async Task<IActionResult> AssignPermission(AssignPermissionDto request)
        {
            var result = await _roleService.AssignPermissionAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPost("remove-perm")]
        public async Task<IActionResult> RemovePermission(AssignPermissionDto request)
        {
            var result = await _roleService.RemovePermissionAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
