namespace Sales.Domain.Entities
{
    public class ProductCategoryAssignment
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int CategoryId { get; private set; }

        // Constructors
        public ProductCategoryAssignment(int productId, int categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }

        private ProductCategoryAssignment() { }
    }
}
