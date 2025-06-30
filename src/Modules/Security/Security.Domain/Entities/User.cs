namespace Security.Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }

        // Core properties
        public string Username { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public int PersonId { get; private set; }
        public int RoleId { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public User(string username, string passwordHash, int personId, int roleId)
        {
            Username = username;
            ChangePassword(passwordHash);
            PersonId = personId;
            RoleId = roleId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private User() { }

        // Public methods
        public void UpdateProfile(string newUsername, int newRoleId)
        {
            if (string.IsNullOrWhiteSpace(newUsername))
                throw new ArgumentException("Username must not be empty or null.");

            Username = newUsername;
            RoleId = newRoleId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
                throw new ArgumentException("Password must not be empty or null.");

            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            if (!IsActive)
                return;

            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            if (!IsActive)
            {
                IsActive = true;
                UpdatedAt = DateTime.UtcNow;
            }
        } 
    }
}
