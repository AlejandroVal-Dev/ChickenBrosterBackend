namespace Security.Application.DTOs.Role
{
    public class UpdateRoleDto
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
