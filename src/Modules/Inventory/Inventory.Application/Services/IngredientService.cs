using Inventory.Application.DTOs.Ingredient;
using Inventory.Application.DTOs.IngredientCategoryAssignment;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Entities;
using SharedKernel.Util;

namespace Inventory.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<IngredientDto>>> GetAllAsync()
        {
            var ingredients = await _unitOfWork.Ingredients.GetAllAsync();
            var result = new List<IngredientDto>();

            foreach (var ingredient in ingredients)
            {
                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ingredient.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<IngredientDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    SKU = ingredient.SKU,
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    UnitCost = ingredient.UnitCost,
                    IsPerishable = ingredient.IsPerishable,
                    ExpirationDate = ingredient.ExpirationDate,
                    IsActive = ingredient.IsActive,
                    CreatedAt = ingredient.CreatedAt,
                    UpdatedAt = ingredient.UpdatedAt
                });
            }

            return Result<IReadOnlyList<IngredientDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<IngredientDto>>> GetActivesAsync()
        {
            var ingredients = await _unitOfWork.Ingredients.GetActivesAsync();
            var result = new List<IngredientDto>();

            foreach (var ingredient in ingredients)
            {
                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ingredient.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<IngredientDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    SKU = ingredient.SKU,
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    UnitCost = ingredient.UnitCost,
                    IsPerishable = ingredient.IsPerishable,
                    ExpirationDate = ingredient.ExpirationDate,
                    IsActive = ingredient.IsActive,
                    CreatedAt = ingredient.CreatedAt,
                    UpdatedAt = ingredient.UpdatedAt
                });
            }

            return Result<IReadOnlyList<IngredientDto>>.Success(result);
        }

        public async Task<Result<IngredientDto>> GetByIdAsync(int id)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(id);
            if (ingredient is null)
                return Result<IngredientDto>.Failure("Ingredient not found.");

            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(id);
            if (unit is null)
                return Result<IngredientDto>.Failure("UnitOfMeasure not found.");

            var dto = new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Description = ingredient.Description,
                SKU = ingredient.SKU,
                UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                UnitCost = ingredient.UnitCost,
                IsPerishable = ingredient.IsPerishable,
                ExpirationDate = ingredient.ExpirationDate,
                IsActive = ingredient.IsActive,
                CreatedAt = ingredient.CreatedAt,
                UpdatedAt = ingredient.UpdatedAt
            };

            return Result<IngredientDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<IngredientDto>>> GetByCategoryIdAsync(int categoryId)
        {
            var ingredients = await _unitOfWork.Ingredients.GetByCategoryAsync(categoryId);
            var result = new List<IngredientDto>();

            foreach (var ingredient in ingredients)
            {
                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ingredient.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<IngredientDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    SKU = ingredient.SKU,
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    UnitCost = ingredient.UnitCost,
                    IsPerishable = ingredient.IsPerishable,
                    ExpirationDate = ingredient.ExpirationDate,
                    IsActive = ingredient.IsActive,
                    CreatedAt = ingredient.CreatedAt,
                    UpdatedAt = ingredient.UpdatedAt
                });
            }

            return Result<IReadOnlyList<IngredientDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<IngredientDto>>> SearchByNameAsync(string name)
        {
            var ingredients = await _unitOfWork.Ingredients.SearchByNameAsync(name);
            var result = new List<IngredientDto>();

            foreach (var ingredient in ingredients)
            {
                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ingredient.UnitOfMeasureId);
                if (unit is null)
                    return Result<IReadOnlyList<IngredientDto>>.Failure("UnitOfMeasure not found.");

                result.Add(new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    SKU = ingredient.SKU,
                    UnitOfMeasureAbbreviation = unit.Abbreviation ?? unit.Name,
                    UnitCost = ingredient.UnitCost,
                    IsPerishable = ingredient.IsPerishable,
                    ExpirationDate = ingredient.ExpirationDate,
                    IsActive = ingredient.IsActive,
                    CreatedAt = ingredient.CreatedAt,
                    UpdatedAt = ingredient.UpdatedAt
                });
            }

            return Result<IReadOnlyList<IngredientDto>>.Success(result);
        }

        public async Task<Result<int>> CreateAsync(CreateIngredientDto request)
        {
            if (await _unitOfWork.Ingredients.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("Ingredient with same name exists.");

            var unitExist = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.UnitOfMeasureId);
            if (unitExist is null)
                return Result<int>.Failure("UnitOfMeasure not found.");

            var ingredient = new Ingredient(
                request.Name,
                request.Description,
                request.SKU,
                request.UnitOfMeasureId,
                request.UnitCost,
                request.IsPerishable,
                request.ExpirationDate
            );

            await _unitOfWork.Ingredients.AddAsync(ingredient);
            await _unitOfWork.CommitAsync();

            var ingredientExist = await _unitOfWork.Ingredients.GetByIdAsync(ingredient.Id);
            if (ingredientExist is null)
                return Result<int>.Failure("Ingredient not found.");

            var inventory = new Domain.Entities.Inventory(
                ingredient.Id,
                request.ActualStock,
                request.MinimumStock
            );

            await _unitOfWork.Inventories.AddAsync(inventory);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(ingredient.Id);
        }

        public async Task<Result> UpdateAsync(UpdateIngredientDto request)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null)
                return Result.Failure("Ingredient not found.");

            ingredient.UpdateInformation(request.Name, request.Description, request.SKU, request.UnitOfMeasureId, request.UnitCost, request.IsPerishable, request.ExpirationDate);
            await _unitOfWork.Ingredients.UpdateAsync(ingredient);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(id);
            if (ingredient is null)
                return Result<IngredientDto>.Failure("Ingredient not found.");

            ingredient.Delete();
            await _unitOfWork.Ingredients.DeactivateAsync(ingredient);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(id);
            if (ingredient is null)
                return Result<IngredientDto>.Failure("Ingredient not found.");

            ingredient.Restore();
            await _unitOfWork.Ingredients.DeactivateAsync(ingredient);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> AssignCategoryAsync(IngredientCategoryAssignmentDto request)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null)
                return Result.Failure("Ingredient not found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            Console.WriteLine(category);
            
            if (category is null)
                return Result.Failure("Category not found.");

            var exist = await _unitOfWork.Ingredients.GetAssignedCategoryIdsAsync(ingredient.Id);
            if (exist.Contains(category.Id))
                return Result.Failure($"{ingredient.Name} is already assigned to {category.Name}");

            var assignment = new IngredientCategoryAssigment(request.IngredientId, request.CategoryId);
            await _unitOfWork.Ingredients.AssignCategoryAsync(assignment);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> UnassignCategoryAsync(IngredientCategoryAssignmentDto request)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null)
                return Result.Failure("Ingredient not found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result.Failure("Category not found.");

            var exist = await _unitOfWork.Ingredients.GetAssignedCategoryIdsAsync(ingredient.Id);
            if (!exist.Contains(category.Id))
                return Result.Failure($"{ingredient.Name} is not assigned to {category.Name}");

            var unassignment = new IngredientCategoryAssigment(request.IngredientId, request.CategoryId);
            await _unitOfWork.Ingredients.UnassignCategoryAsync(unassignment);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
