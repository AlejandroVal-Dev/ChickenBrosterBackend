namespace Security.Domain.Entities
{
    public class Role
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public string? Description { get; private set; }
        public bool IsSystem { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Constructors
        public Role(string name, string? description)
        {
            Name = name;
            Description = description;
            IsSystem = false;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Role() { }

        // Public methods
        public void Rename(string? newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Role name must not be empty or null.");

            Name = newName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateDescription(string? description)
        {
            Description = description;
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
