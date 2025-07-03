using Inventory.Application.DTOs.IngredientCategory;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Entities;
using SharedKernel.Util;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Inventory.Application.Services
{
    public class IngredientCategoryService : IIngredientCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var result = categories.Select(c => new IngredientCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<IngredientCategoryDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetActivesAsync()
        {
            var categories = await _unitOfWork.Categories.GetActivesAsync();
            var result = categories.Select(c => new IngredientCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<IngredientCategoryDto>>.Success(result);
        }

        public async Task<Result<IngredientCategoryDto>> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return Result<IngredientCategoryDto>.Failure("Category not found.");

            var dto = new IngredientCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            return Result<IngredientCategoryDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<IngredientCategoryDto>>> GetByIngredientIdAsync(int ingredientId)
        {
            var categories = await _unitOfWork.Categories.GetByAssignedIngredientIdAsync(ingredientId);
            var result = categories.Select(c => new IngredientCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<IngredientCategoryDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<IngredientCategoryDto>>> SearchByNameAsync(string name)
        {
            var categories = await _unitOfWork.Categories.SearchByNameAsync(name);
            var result = categories.Select(c => new IngredientCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<IngredientCategoryDto>>.Success(result);
        }

        public async Task<Result<int>> CreateAsync(CreateIngredientCategoryDto request)
        { 
            if (await _unitOfWork.Categories.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("Category with same name exists.");

            var category = new IngredientCategory(
                request.Name
            );

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(category.Id);
        }

        public async Task<Result> UpdateAsync(UpdateIngredientCategoryDto request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result.Failure("Category not found.");

            category.UpdateName(request.Name);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return Result.Failure("Category not found.");

            category.Delete();
            await _unitOfWork.Categories.DeactivateAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category is null)
                return Result.Failure("Category not found.");

            category.Restore();
            await _unitOfWork.Categories.RestoreAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<int>>> GetAssignedByIngredient(int ingredientId)
        {
            var categoryIds = await _unitOfWork.Ingredients.GetAssignedCategoryIdsAsync(ingredientId);

            if (categoryIds == null || categoryIds.Count == 0)
                return Result<IReadOnlyList<int>>.Failure("No se encontraron categorías asignadas.");

            return Result<IReadOnlyList<int>>.Success(categoryIds);
        }
    }
}
