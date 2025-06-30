using SharedKernel.Enums;

namespace Security.Application.DTOs.User
{
    public class CreateUserDto
    {
        // User
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }

        // Person
        public string Name { get; set; } = null!;
        public string LastName1 { get; set; } = null!;
        public string? LastName2 { get; set; }
        public string DocumentId { get; set; } = null!;
        public DocumentType DocumentType { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
    }
}
