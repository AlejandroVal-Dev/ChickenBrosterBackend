using Sales.Application.DTOs.ProductRecipe;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class ProductRecipeService : IProductRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductRecipeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<ProductRecipeDto>>> GetAllAsync()
        {
            var recipes = await _unitOfWork.ProductRecipes.GetAllAsync();
            var result = recipes.Select(pr => new ProductRecipeDto
            {
                Id = pr.Id,
                ProductId = pr.ProductId,
                RecipeId = pr.RecipeId,
                Quantity = pr.Quantity,
                IsActive = pr.IsActive,
                CreatedAt = pr.CreatedAt,
                UpdatedAt = pr.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductRecipeDto>>.Success(result);
        }

        public async Task<Result<ProductRecipeDto>> GetByProductIdAsync(int id)
        {
            var recipe = await _unitOfWork.ProductRecipes.GetByProductIdAsync(id);
            if (recipe is null)
                return Result<ProductRecipeDto>.Failure("ProductRecipe not found.");

            var dto = new ProductRecipeDto
            {
                Id = recipe.Id,
                ProductId = recipe.ProductId,
                RecipeId = recipe.RecipeId,
                Quantity = recipe.Quantity,
                IsActive = recipe.IsActive,
                CreatedAt = recipe.CreatedAt,
                UpdatedAt = recipe.UpdatedAt
            };

            return Result<ProductRecipeDto>.Success(dto);
        }

        public async Task<Result<int>> CreateAsync(CreateProductRecipeDto request)
        {
            var recipe = await _unitOfWork.ProductRecipes.GetByProductIdAsync(request.ProductId);
            if (recipe is not null)
                return Result<int>.Failure("Product has a recipe assigned.");

            var dto = new ProductRecipe(
                request.ProductId,
                request.RecipeId,
                request.Quantity
            );

            await _unitOfWork.ProductRecipes.AddAsync(dto);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(dto.Id);
        }

        public async Task<Result> UpdateQuantityAsync(UpdateProductRecipeDto request)
        {
            var recipe = await _unitOfWork.ProductRecipes.GetByProductIdAsync(request.ProductId);
            if (recipe is null)
                return Result.Failure("ProductRecipe not found");

            recipe.UpdateQuantity(request.NewQuantity);
            await _unitOfWork.ProductRecipes.UpdateAsync(recipe);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteRecipeAsync(int productRecipeId)
        {
            var recipe = await _unitOfWork.ProductRecipes.GetByProductIdAsync(productRecipeId);
            if (recipe is null)
                return Result.Failure("ProductRecipe not found");

            recipe.Delete();
            await _unitOfWork.ProductRecipes.DeactivateAsync(recipe);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreRecipeAsync(int productRecipeId)
        {
            var recipe = await _unitOfWork.ProductRecipes.GetByProductIdAsync(productRecipeId);
            if (recipe is null)
                return Result.Failure("ProductRecipe not found");

            recipe.Restore();
            await _unitOfWork.ProductRecipes.RestoreAsync(recipe);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
