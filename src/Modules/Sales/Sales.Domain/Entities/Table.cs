namespace Sales.Domain.Entities
{
    public class Table
    {
        public int Id { get; private set; }

        // Core properties
        public string Number { get; private set; } = null!;
        public bool IsAvailable { get; private set; }

        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        // Constructor
        public Table(string number)
        {
            Number = number;
            IsAvailable = true;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Table() { }

        // Public methods
        public void MarkAsOccupied()
        {
            if (!IsAvailable)
                return;

            IsAvailable = false;
        }

        public void MarkAsAvailable()
        {
            if (!IsAvailable)
            {
                IsAvailable = true;
            }
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
