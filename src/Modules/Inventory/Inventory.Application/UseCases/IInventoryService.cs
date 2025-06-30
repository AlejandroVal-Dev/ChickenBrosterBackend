using Inventory.Application.DTOs.Inventory;
using SharedKernel.Util;

namespace Inventory.Application.UseCases
{
    public interface IInventoryService
    {
        Task<Result<IReadOnlyList<InventoryDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<InventoryDto>>> GetActivesAsync();
        Task<Result<InventoryDto>> GetByIngredientIdAsync(int ingredientId);
        Task<Result<IReadOnlyList<InventoryDto>>> GetLowStockAsync();
        Task<Result> UpdateMinimumStockAsync(UpdateInventoryMinimumStockDto request);
    }
}
