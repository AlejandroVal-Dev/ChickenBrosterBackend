using Sales.Domain.Repositories;

namespace Sales.Application.Util
{
    public interface IUnitOfWork
    {
        IOrderRepository Orders { get; }
        IProductRepository Products { get; }
        IPaymentRepository Payments { get; }
        ITableRepository Tables {  get; }
        IProductCategoryRepository Categories { get; }
        IOrderItemRepository OrderItems { get; }
        IProductRecipeRepository ProductRecipes { get; }
        Task<int> CommitAsync();
    }
}
