using Inventory.Application.DTOs.InventoryMovement;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IInventoryMovementService
    {
        Task<Result<IReadOnlyList<InventoryMovementDto>>> GetAllAsync();
        Task<Result<InventoryMovementDto>> GetByIdAsync(int id);
        Task<Result<IReadOnlyList<InventoryMovementDto>>> GetFilteredAsync(InventoryMovementFilterDto request);
        Task<Result<int>> CreateAsync(CreateInventoryMovementDto request);
    }
}
