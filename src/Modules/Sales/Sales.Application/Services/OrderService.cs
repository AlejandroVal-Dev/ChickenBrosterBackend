using Sales.Application.DTOs.Order;
using Sales.Application.DTOs.OrderItem;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using Sales.Domain.Repositories;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<OrderDto>>> GetAllAsync()
        {
            var orders = await _unitOfWork.Orders.GetAllAsync();
            var result = new List<OrderDto>();

            foreach (var order in orders)
            {
                var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);
                var dtos = new List<OrderItemDto>();

                foreach (var item in items)
                { 
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    dtos.Add(new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = product?.Name ?? "N/A",
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    });
                }

                result.Add(new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TableId = order.TableId,
                    OrderType = order.OrderType,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,
                    UpdatedAt = order.UpdatedAt,
                    Items = dtos
                });
            }

            return Result<IReadOnlyList<OrderDto>>.Success(result);
        }

        public async Task<Result<OrderDto>> GetByIdAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null)
                return Result<OrderDto>.Failure("Order not found");

            var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);
            var itemDtos = new List<OrderItemDto>();

            foreach (var item in items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                itemDtos.Add(new OrderItemDto
                {
                    ProductId = item.ProductId,
                    ProductName = product?.Name ?? "N/A",
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice
                });
            }

            var dto = new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                TableId = order.TableId,
                OrderType = order.OrderType,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                Items = itemDtos
            };

            return Result<OrderDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<OrderDto>>> GetFilteredAsync(OrderFilterDto filter)
        {
            var orders = await _unitOfWork.Orders.GetFilteredAsync(filter.OrderType, filter.Status);
            var result = new List<OrderDto>();

            foreach (var order in orders)
            {
                var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);
                var dtos = new List<OrderItemDto>();

                foreach (var item in items)
                {
                    var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                    dtos.Add(new OrderItemDto
                    {
                        ProductId = item.ProductId,
                        ProductName = product?.Name ?? "N/A",
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    });
                }

                result.Add(new OrderDto
                {
                    Id = order.Id,
                    UserId = order.UserId,
                    TableId = order.TableId,
                    OrderType = order.OrderType,
                    Status = order.Status,
                    TotalAmount = order.TotalAmount,
                    CreatedAt = order.CreatedAt,
                    UpdatedAt = order.UpdatedAt,
                    Items = dtos
                });
            }

            return Result<IReadOnlyList<OrderDto>>.Success(result);
        }

        public async Task<Result<int>> CreateOrderAsync(CreateOrderDto request)
        {
            var order = new Order(request.UserId, request.TableId, request.OrderType);
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.CommitAsync();

            decimal total = 0m;

            foreach (var item in request.Items)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(item.ProductId);
                if (product == null)
                    return Result<int>.Failure($"Product with id {item.ProductId} not found");

                var detail = new OrderItem(order.Id, product.Id, item.Quantity, product.Price);
                await _unitOfWork.OrderItems.AddAsync(detail);
                await _unitOfWork.CommitAsync();

                total += detail.TotalPrice;
            }

            order.UpdateTotalAmount(total);
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(order.Id);
        }

        public async Task<Result> MarkAsPaidAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order is null)
                return Result.Failure("Order not found");

            order.MarkAsPaid();
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> CancelAsync(int orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order is null)
                return Result.Failure("Order not found");

            order.Cancel();
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> AddItemAsync(int orderId, AddOrderItemDto request)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order is null)
                return Result.Failure("Order not found");

            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure("Product not found");

            var detail = new OrderItem(order.Id, product.Id, request.Quantity, product.Price);
            await _unitOfWork.OrderItems.AddAsync(detail);
            await _unitOfWork.CommitAsync();

            var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);
            var total = items.Sum(x => x.TotalPrice);

            order.UpdateTotalAmount(total);
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> UpdateItemQuantityAsync(UpdateOrderItemDto request)
        {
            var detail = await _unitOfWork.OrderItems.GetByOrderAndProductAsync(request.OrderId, request.ProductId);
            if (detail is null)
                return Result.Failure("Order item not found");

            detail.UpdateQuantity(request.NewQuantity);
            await _unitOfWork.OrderItems.UpdateAsync(detail);
            await _unitOfWork.CommitAsync();

            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order is null)
                return Result.Failure("Order not found");

            var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);

            var total = items.Sum(x => x.TotalPrice);

            order.UpdateTotalAmount(total);
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RemoveItemAsync(RemoveOrderItemDto request)
        {
            var detail = await _unitOfWork.OrderItems.GetByOrderAndProductAsync(request.OrderId, request.ProductId);
            if (detail is null)
                return Result.Failure("Order item not found");

            await _unitOfWork.OrderItems.DeleteAsync(detail);
            await _unitOfWork.CommitAsync();

            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
            if (order is null)
                return Result.Failure("Order not found");

            var items = await _unitOfWork.OrderItems.GetByOrderIdAsync(order.Id);

            var total = items.Sum(x => x.TotalPrice);

            order.UpdateTotalAmount(total);
            await _unitOfWork.Orders.UpdateAsync(order);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
