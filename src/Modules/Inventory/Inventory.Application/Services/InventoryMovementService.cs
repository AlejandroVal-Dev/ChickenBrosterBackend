using Inventory.Application.DTOs.InventoryMovement;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using SharedKernel.Util;

namespace Inventory.Application.Services
{
    public class InventoryMovementService : IInventoryMovementService
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryMovementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<InventoryMovementDto>>> GetAllAsync()
        {
            var movements = await _unitOfWork.InventoryMovements.GetAllAsync();
            var result = new List<InventoryMovementDto>();

            foreach (var movement in movements)
            { 
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(movement.IngredientId);
                if (ingredient is null)
                    return Result<IReadOnlyList<InventoryMovementDto>>.Failure("Ingredient not found.");

                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(movement.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<InventoryMovementDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new InventoryMovementDto
                {
                    Id = movement.Id,
                    IngredientName = ingredient.Name ?? "(Eliminado)",
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    Quantity = movement.Quantity,
                    MovementType = movement.MovementType.ToString(),
                    MadeByUserId = movement.MadeByUserId,
                    Reason = movement.Reason,
                    MovementDate = movement.MovementDate,
                });
            }

            return Result<IReadOnlyList<InventoryMovementDto>>.Success(result);
        }

        public async Task<Result<InventoryMovementDto>> GetByIdAsync(int id)
        {
            var movement = await _unitOfWork.InventoryMovements.GetByIdAsync(id);
            if (movement is null)
                return Result<InventoryMovementDto>.Failure("Movement does not exist.");

            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(movement.IngredientId);
            if (ingredient is null)
                return Result<InventoryMovementDto>.Failure("Ingredient not found.");

            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(movement.UnitOfMeasureId);
            if (unit is null)
                return Result<InventoryMovementDto>.Failure("UnitOfMeasure not found.");

            var dto = new InventoryMovementDto
            {
                Id = movement.Id,
                IngredientName = ingredient.Name ?? "(Eliminado)",
                UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                Quantity = movement.Quantity,
                MovementType = movement.MovementType.ToString(),
                MadeByUserId = movement.MadeByUserId,
                Reason = movement.Reason,
                MovementDate = movement.MovementDate,
            };

            return Result<InventoryMovementDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<InventoryMovementDto>>> GetFilteredAsync(InventoryMovementFilterDto request)
        {
            var movements = await _unitOfWork.InventoryMovements.GetFilteredAsync(request.IngredientId, request.From, request.To, request.MovementType, request.MadeByUserId);
            var result = new List<InventoryMovementDto>();

            foreach (var movement in movements)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(movement.IngredientId);
                if (ingredient is null)
                    return Result<IReadOnlyList<InventoryMovementDto>>.Failure("Ingredient not found.");

                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(movement.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<InventoryMovementDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new InventoryMovementDto
                {
                    Id = movement.Id,
                    IngredientName = ingredient.Name ?? "(Eliminado)",
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    Quantity = movement.Quantity,
                    MovementType = movement.MovementType.ToString(),
                    MadeByUserId = movement.MadeByUserId,
                    Reason = movement.Reason,
                    MovementDate = movement.MovementDate,
                });
            }

            return Result<IReadOnlyList<InventoryMovementDto>>.Success(result);
        }

        public async Task<Result<int>> CreateAsync(CreateInventoryMovementDto request)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null)
                return Result<int>.Failure("Ingredient not found.");

            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.UnitOfMeasureId);
            if (unit is null)
                return Result<int>.Failure("UnitOfMeasure not found.");

            var inventory = await _unitOfWork.Inventories.GetByIngredientIdAsync(request.IngredientId);
            if (inventory is null)
                return Result<int>.Failure("Inventory not found for ingredient.");

            var movement = new InventoryMovement(
                ingredientId: request.IngredientId,
                unitOfMeasureId: request.UnitOfMeasureId,
                quantity: request.Quantity,
                movementType: request.MovementType,
                reason: request.Reason,
                madeByUserId: request.MadeByUserId
            );

            if (!movement.IsValid())
                return Result<int>.Failure("Quantity must be greater than 0");

            await _unitOfWork.InventoryMovements.AddAsync(movement);

            switch (request.MovementType)
            {
                case MovementType.Income:
                    inventory.Increase(request.Quantity);
                    break;
                case MovementType.Outcome:
                    inventory.Decrease(request.Quantity);
                    break;
                case MovementType.Adjust:
                    inventory.Fit(request.Quantity);
                    break;
                default:
                    return Result<int>.Failure("Invalid movement type.");
            }

            await _unitOfWork.Inventories.UpdateAsync(inventory);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(movement.Id);
        }
    }
}
