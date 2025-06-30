namespace Security.Application.DTOs.Login
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
        public int RoleId { get; set; }
    }
}
