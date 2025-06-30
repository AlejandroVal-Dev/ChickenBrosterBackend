using Sales.Application.DTOs.Order;
using Sales.Application.DTOs.OrderItem;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface IOrderService
    {
        Task<Result<IReadOnlyList<OrderDto>>> GetAllAsync();
        Task<Result<OrderDto>> GetByIdAsync(int orderId);
        Task<Result<IReadOnlyList<OrderDto>>> GetFilteredAsync(OrderFilterDto filter);
        Task<Result<int>> CreateOrderAsync(CreateOrderDto request);
        Task<Result> MarkAsPaidAsync(int orderId);
        Task<Result> CancelAsync(int orderId);
        Task<Result> AddItemAsync(int orderId, AddOrderItemDto request);
        Task<Result> UpdateItemQuantityAsync(UpdateOrderItemDto request);
        Task<Result> RemoveItemAsync(RemoveOrderItemDto request);
    }
}
