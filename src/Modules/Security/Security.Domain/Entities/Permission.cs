namespace Security.Domain.Entities
{
    public class Permission
    {
        public int Id { get; private set; }

        // Core properties
        public string Code { get; private set; } = null!;
        public string? Description { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Constructors
        public Permission(string code, string? description)
        {
            Code = code;
            Description = description;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Permission() { }

        // Public methods
        public void UpdateDescription(string? newDescription)
        {
            Description = newDescription;
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
