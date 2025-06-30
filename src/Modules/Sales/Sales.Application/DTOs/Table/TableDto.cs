namespace Sales.Application.DTOs.Table
{
    public class TableDto
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public bool IsAvailable { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
