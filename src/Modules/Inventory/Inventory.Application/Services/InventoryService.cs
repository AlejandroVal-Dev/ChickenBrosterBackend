using Inventory.Application.DTOs.Inventory;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using SharedKernel.Util;

namespace Inventory.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<InventoryDto>>> GetAllAsync()
        {
            var inventories = await _unitOfWork.Inventories.GetAllAsync();
            var result = new List<InventoryDto>();

            foreach (var inventory in inventories)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(inventory.IngredientId);
                if (ingredient is null)
                    return Result<IReadOnlyList<InventoryDto>>.Failure("Ingredient not found.");

                result.Add(new InventoryDto
                {
                    Id = inventory.Id,
                    IngredientName = ingredient.Name,
                    ActualStock = inventory.ActualStock,
                    MinimumStock = inventory.MinimumStock,
                    UnderMinimum = inventory.UnderMinimum(),
                    LastMovement = inventory.LastMovement,
                    IsActive = inventory.IsActive,
                    CreatedAt = inventory.CreatedAt,
                    UpdatedAt = inventory.UpdatedAt
                });
            }

            return Result<IReadOnlyList<InventoryDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<InventoryDto>>> GetActivesAsync()
        {
            var inventories = await _unitOfWork.Inventories.GetActivesAsync();
            var result = new List<InventoryDto>();

            foreach (var inventory in inventories)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(inventory.IngredientId);
                if (ingredient is null)
                    return Result<IReadOnlyList<InventoryDto>>.Failure("Ingredient not found.");

                result.Add(new InventoryDto
                {
                    Id = inventory.Id,
                    IngredientName = ingredient.Name,
                    ActualStock = inventory.ActualStock,
                    MinimumStock = inventory.MinimumStock,
                    UnderMinimum = inventory.UnderMinimum(),
                    LastMovement = inventory.LastMovement,
                    IsActive = inventory.IsActive,
                    CreatedAt = inventory.CreatedAt,
                    UpdatedAt = inventory.UpdatedAt
                });
            }

            return Result<IReadOnlyList<InventoryDto>>.Success(result);
        }

        public async Task<Result<InventoryDto>> GetByIngredientIdAsync(int ingredientId)
        {
            var inventory = await _unitOfWork.Inventories.GetByIngredientIdAsync(ingredientId);
            if (inventory is null)
                return Result<InventoryDto>.Failure("Inventory not found.");

            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(inventory.IngredientId);
            if (ingredient is null)
                return Result<InventoryDto>.Failure("Ingredient not found.");

            var dto = new InventoryDto
            {
                Id = inventory.Id,
                IngredientName = ingredient.Name,
                ActualStock = inventory.ActualStock,
                MinimumStock = inventory.MinimumStock,
                UnderMinimum = inventory.UnderMinimum(),
                LastMovement = inventory.LastMovement,
                IsActive = inventory.IsActive,
                CreatedAt = inventory.CreatedAt,
                UpdatedAt = inventory.UpdatedAt
            };

            return Result<InventoryDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<InventoryDto>>> GetLowStockAsync()
        {
            var lowStockList = await _unitOfWork.Inventories.GetLowStockAsync();
            var result = new List<InventoryDto>();

            foreach (var inventory in lowStockList)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(inventory.IngredientId);
                if (ingredient is null)
                    return Result<IReadOnlyList<InventoryDto>>.Failure("Ingredient not found.");

                result.Add(new InventoryDto
                {
                    Id = inventory.Id,
                    IngredientName = ingredient.Name,
                    ActualStock = inventory.ActualStock,
                    MinimumStock = inventory.MinimumStock,
                    UnderMinimum = inventory.UnderMinimum(),
                    LastMovement = inventory.LastMovement,
                    IsActive = inventory.IsActive,
                    CreatedAt = inventory.CreatedAt,
                    UpdatedAt = inventory.UpdatedAt
                });
            }

            return Result<IReadOnlyList<InventoryDto>>.Success(result);
        }

        public async Task<Result> UpdateMinimumStockAsync(UpdateInventoryMinimumStockDto request)
        {
            var inventory = await _unitOfWork.Inventories.GetByIngredientIdAsync(request.IngredientId);
            if (inventory is null)
                return Result.Failure("Inventory not found.");

            inventory.UpdateMinimumStock(request.MinimumStock);
            await _unitOfWork.Inventories.UpdateAsync(inventory);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
