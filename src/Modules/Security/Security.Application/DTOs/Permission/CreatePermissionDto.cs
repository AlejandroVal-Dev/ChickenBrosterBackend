namespace Security.Application.DTOs.Permission
{
    public class CreatePermissionDto
    {
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
    }
}
