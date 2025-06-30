namespace Security.Application.DTOs.User
{
    public class UpdateUserDto
    {
        public int UserId { get; set; }
        public string NewUsername { get; set; } = null!;
        public int NewRoleId { get; set; }
    }
}
