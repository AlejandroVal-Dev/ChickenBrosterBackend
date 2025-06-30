using Inventory.Application.DTOs.Recipe;
using Inventory.Application.DTOs.RecipeIngredient;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Entities;
using SharedKernel.Util;

namespace Inventory.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<RecipeDto>>> GetAllAsync()
        {
            var recipes = await _unitOfWork.Recipes.GetAllAsync();
            var result = new List<RecipeDto>();

            foreach (var recipe in recipes)
            {
                var dto = new RecipeDto
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    IsActive = recipe.IsActive,
                    CreatedAt = recipe.CreatedAt,
                    UpdatedAt = recipe.UpdatedAt,
                    Ingredients = new List<RecipeIngredientDto>()
                };

                foreach (var ri in recipe.Ingredients)
                {
                    var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(ri.IngredientId);
                    if (ingredient is null)
                        return Result<IReadOnlyList<RecipeDto>>.Failure("Ingredient not found.");

                    var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ri.UnitOfMeasureId);
                    if (unit is null)
                        return Result<IReadOnlyList<RecipeDto>>.Failure("UnitOfMeasure not found.");

                    dto.Ingredients.Add(new RecipeIngredientDto
                    {
                        IngredientId = ri.IngredientId,
                        IngredientName = ingredient?.Name ?? "Desconocido",
                        Quantity = ri.Quantity,
                        UnitOfMeasureAbbreviation = (unit?.Abbreviation ?? unit?.Name) ?? "?"
                    });
                }

                result.Add(dto);
            }

            return Result<IReadOnlyList<RecipeDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<RecipeDto>>> GetActivesAsync()
        {
            var recipes = await _unitOfWork.Recipes.GetActivesAsync();
            var result = new List<RecipeDto>();

            foreach (var recipe in recipes)
            {
                var dto = new RecipeDto
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    IsActive = recipe.IsActive,
                    CreatedAt = recipe.CreatedAt,
                    UpdatedAt = recipe.UpdatedAt,
                    Ingredients = new List<RecipeIngredientDto>()
                };

                foreach (var ri in recipe.Ingredients)
                {
                    var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(ri.IngredientId);
                    if (ingredient is null)
                        return Result<IReadOnlyList<RecipeDto>>.Failure("Ingredient not found.");

                    var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ri.UnitOfMeasureId);
                    if (unit is null)
                        return Result<IReadOnlyList<RecipeDto>>.Failure("UnitOfMeasure not found.");

                    dto.Ingredients.Add(new RecipeIngredientDto
                    {
                        IngredientId = ri.IngredientId,
                        IngredientName = ingredient?.Name ?? "Desconocido",
                        Quantity = ri.Quantity,
                        UnitOfMeasureAbbreviation = (unit?.Abbreviation ?? unit?.Name) ?? "?"
                    });
                }

                result.Add(dto);
            }

            return Result<IReadOnlyList<RecipeDto>>.Success(result);
        }

        public async Task<Result<RecipeDto>> GetByIdAsync(int id)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(id);
            if (recipe is null)
                return Result<RecipeDto>.Failure("Recipe not found.");

            var dto = new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                IsActive = recipe.IsActive,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Ingredients = new List<RecipeIngredientDto>()
            };

            foreach (var ri in recipe.Ingredients)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(ri.IngredientId);
                if (ingredient is null)
                    return Result<RecipeDto>.Failure("Ingredient not found.");

                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ri.UnitOfMeasureId);
                if (unit is null)
                    return Result<RecipeDto>.Failure("UnitOfMeasure not found.");

                dto.Ingredients.Add(new RecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ingredient?.Name ?? "Desconocido",
                    Quantity = ri.Quantity,
                    UnitOfMeasureAbbreviation = (unit?.Abbreviation ?? unit?.Name) ?? "?" 
                });
            }

            return Result<RecipeDto>.Success(dto);
        }

        public async Task<Result<RecipeDto>> GetByNameAsync(string name)
        {
            var recipe = await _unitOfWork.Recipes.GetByNameAsync(name);
            if (recipe is null)
                return Result<RecipeDto>.Failure("Recipe not found.");

            var dto = new RecipeDto
            {
                Id = recipe.Id,
                Name = recipe.Name,
                IsActive = recipe.IsActive,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt,
                Ingredients = new List<RecipeIngredientDto>()
            };

            foreach (var ri in recipe.Ingredients)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(ri.IngredientId);
                if (ingredient is null)
                    return Result<RecipeDto>.Failure("Ingredient not found.");

                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ri.UnitOfMeasureId);
                if (unit is null)
                    return Result<RecipeDto>.Failure("UnitOfMeasure not found.");

                dto.Ingredients.Add(new RecipeIngredientDto
                {
                    IngredientId = ri.IngredientId,
                    IngredientName = ingredient?.Name ?? "Desconocido",
                    Quantity = ri.Quantity,
                    UnitOfMeasureAbbreviation = (unit?.Abbreviation ?? unit?.Name) ?? "?"
                });
            }

            return Result<RecipeDto>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(CreateRecipeDto request)
        {
            var existing = await _unitOfWork.Recipes.GetByNameAsync(request.Name);
            if (existing != null)
                return Result<int>.Failure("Recipe with same name exists.");

            var recipe = new Recipe(request.Name);

            foreach (var ingredientRequest in request.Ingredients)
            {
                var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(ingredientRequest.IngredientId);
                if (ingredient is null || !ingredient.IsActive)
                    return Result<int>.Failure($"Ingredient {ingredientRequest.IngredientId} not found.");

                var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(ingredientRequest.UnitOfMeasureId);
                if (unit is null || !unit.IsActive)
                    return Result<int>.Failure($"UnitOfMeasure {ingredientRequest.UnitOfMeasureId} not found.");

                recipe.AddIngredient(ingredientRequest.IngredientId, ingredientRequest.Quantity, ingredientRequest.UnitOfMeasureId);
            }

            await _unitOfWork.Recipes.AddAsync(recipe);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(recipe.Id);
        }

        public async Task<Result> UpdateAsync(UpdateRecipeDto request)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(request.RecipeId);
            if (recipe is null)
                return Result.Failure("Recipe not found.");

            recipe.UpdateInformation(request.Name);
            await _unitOfWork.Recipes.UpdateAsync(recipe);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(id);
            if (recipe is null)
                return Result.Failure("Recipe not found.");

            recipe.Delete();
            await _unitOfWork.Recipes.DeactivateAsync(recipe);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(id);
            if (recipe is null)
                return Result.Failure("Recipe not found.");

            recipe.Delete();
            await _unitOfWork.Recipes.RestoreAsync(recipe);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }

        public async Task<Result> AddIngredientAsync(int recipeId, CreateRecipeIngredientDto request)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe is null || !recipe.IsActive)
                return Result.Failure("Recipe not found.");

            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null || !ingredient.IsActive)
                return Result.Failure("Ingredient not found.");

            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.UnitOfMeasureId);
            if (unit is null || !unit.IsActive)
                return Result.Failure("UnitOfMeasure not found.");

            recipe.AddIngredient(request.IngredientId, request.Quantity, request.UnitOfMeasureId);
            await _unitOfWork.Recipes.UpdateAsync(recipe);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }

        public async Task<Result> RemoveIngredientAsync(int recipeId, int ingredientId)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe is null)
                return Result.Failure("Recipe not found.");

            recipe.RemoveIngredient(ingredientId);
            await _unitOfWork.Recipes.UpdateAsync(recipe);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }

        public async Task<Result> UpdateIngredientQuantityAsync(int recipeId, UpdateRecipeIngredientQuantityDto request)
        {
            var recipe = await _unitOfWork.Recipes.GetByIdAsync(recipeId);
            if (recipe is null || !recipe.IsActive)
                return Result.Failure("Recipe not found.");

            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(request.IngredientId);
            if (ingredient is null || !ingredient.IsActive)
                return Result.Failure("Ingredient not found.");

            var ingredientInRecipe = recipe.Ingredients.FirstOrDefault(i => i.IngredientId == request.IngredientId);
            if (ingredientInRecipe is null)
                return Result.Failure($"Ingredient {ingredient.Name} is not in recipe {recipe.Name}");

            recipe.UpdateIngredientQuantity(request.IngredientId, request.NewQuantity);
            await _unitOfWork.Recipes.UpdateAsync(recipe);
            await _unitOfWork.CommitAsync();
            return Result.Success();
        }
    }
}
