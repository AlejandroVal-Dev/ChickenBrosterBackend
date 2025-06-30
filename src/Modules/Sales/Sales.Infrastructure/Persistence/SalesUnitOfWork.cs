using Sales.Application.Util;
using Sales.Domain.Repositories;
using Sales.Infrastructure.Repository;

namespace Sales.Infrastructure.Persistence
{
    public class SalesUnitOfWork : IUnitOfWork
    {
        private readonly SalesDbContext _database;
        public IOrderRepository Orders { get; }

        public IProductRepository Products { get; }

        public IPaymentRepository Payments { get; }

        public ITableRepository Tables { get; }

        public IProductCategoryRepository Categories { get; }

        public IOrderItemRepository OrderItems { get; }

        public IProductRecipeRepository ProductRecipes { get; }

        public SalesUnitOfWork(SalesDbContext database)
        {
            _database = database;
            Orders = new OrderRepository(_database);
            Products = new ProductRepository(_database);
            Payments = new PaymentRepository(_database);
            Tables = new TableRepository(_database);
            Categories = new ProductCategoryRepository(_database);
            OrderItems = new OrderItemRepository(_database);
            ProductRecipes = new ProductRecipeRepository(_database);
        }

        public Task<int> CommitAsync() => _database.SaveChangesAsync();

    }
}
