using SharedKernel.Enums;

namespace SharedKernel.Entities
{
    public class Person
    {
        public int Id { get; private set; }

        // Core properties
        public string Name { get; private set; } = null!;
        public string LastName1 { get; private set; } = null!;
        public string? LastName2 { get; private set; }
        public string DocumentId { get; private set; } = null!;
        public DocumentType DocumentType { get; private set; }
        public string? PhoneNumber { get; private set; }
        public string? Email { get; private set; }
        public PersonType PersonType { get; private set; }
       
        // Audit properties
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        // Constructors
        public Person(string name, string lastName1, string? lastName2, string documentId, DocumentType documentType, string? phoneNumber, string? email, PersonType personType)
        {
            Name = name;
            LastName1 = lastName1;
            LastName2 = lastName2;
            DocumentId = documentId;
            DocumentType = documentType;
            PhoneNumber = phoneNumber;
            Email = email;
            PersonType = personType;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        private Person() { }

        // Public methods
        public string GetFullName()
        {
            return string.Join(" ", new[] { Name, LastName1, LastName2 }
                .Where(x => !string.IsNullOrWhiteSpace(x)));
        }

        public void UpdateName(string name, string lastName1, string? lastName2)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName1))
                throw new ArgumentException("Firstname & Lastname must not be empty or null.");

            Name = name;
            LastName1 = lastName1;
            LastName2 = lastName2;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateContact(string? email, string? phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void ChangeDocument(string newDocumentId, DocumentType newDocumentType)
        {
            if (string.IsNullOrWhiteSpace(newDocumentId))
                throw new ArgumentException("DocumentId must not be empty or null.");

            DocumentId = newDocumentId;
            DocumentType = newDocumentType;
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
