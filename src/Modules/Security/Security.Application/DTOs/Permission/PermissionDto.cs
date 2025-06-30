namespace Security.Application.DTOs.Permission
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
