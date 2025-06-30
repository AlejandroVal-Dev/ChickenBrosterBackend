using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.DTOs.Permission;
using Security.Application.UseCases;
using SharedKernel.Util;
using System.Collections.Generic;

namespace Security.Api.Controllers
{
    [Route("security/[controller]")]
    [ApiController]
    [Authorize]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionsController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _permissionService.GetAllAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActives()
        {
            var result = await _permissionService.GetActivesAsync();

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _permissionService.GetByIdAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByCode([FromQuery] string code)
        {
            var result = await _permissionService.SearchByCodeAsync(code);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePermissionDto request)
        {
            var result = await _permissionService.CreateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            var createdPermission = await _permissionService.GetByIdAsync(result.Value);
            return CreatedAtAction(nameof(GetById), new { id = result.Value }, createdPermission.Value);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(UpdatePermissionDto request)
        {
            var result = await _permissionService.UpdateAsync(request);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpDelete("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            var result = await _permissionService.DeleteAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore(int id)
        {
            var result = await _permissionService.RestoreAsync(id);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return NoContent();
        }
    }
}
