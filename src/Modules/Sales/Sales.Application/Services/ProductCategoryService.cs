using Sales.Application.DTOs.ProductCategory;
using Sales.Application.DTOs.Table;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<ProductCategoryDto>>> GetAllAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();
            var result = categories.Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductCategoryDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<ProductCategoryDto>>> GetActivesAsync()
        {
            var categories = await _unitOfWork.Categories.GetActivesAsync();
            var result = categories.Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductCategoryDto>>.Success(result);
        }

        public async Task<Result<ProductCategoryDto>> GetByIdAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category is null)
                return Result<ProductCategoryDto>.Failure("Category not found");

            var dto = new ProductCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                ParentCategoryId = category.ParentCategoryId,
                IsActive = category.IsActive,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.UpdatedAt
            };

            return Result<ProductCategoryDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<ProductCategoryDto>>> SearchByNameAsync(string name)
        {
            var categories = await _unitOfWork.Categories.SearchByNameAsync(name);
            var result = categories.Select(c => new ProductCategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                ParentCategoryId = c.ParentCategoryId,
                IsActive = c.IsActive,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductCategoryDto>>.Success(result);
        }

        public async Task<Result<int>> CreateCategoryAsync(CreateProductCategoryDto request)
        {
            if (await _unitOfWork.Categories.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("Category with same number exists.");

            var category = new ProductCategory(
                request.Name,
                request.ParentCategoryId
            );

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(category.Id);
        }

        public async Task<Result> UpdateCategoryAsync(UpdateProductCategoryDto request)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result<ProductCategoryDto>.Failure("Category not found");

            category.UpdateName(request.NewName);
            await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category is null)
                return Result<ProductCategoryDto>.Failure("Category not found");

            category.Delete();
            await _unitOfWork.Categories.DeactivateAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreCategoryAsync(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category is null)
                return Result<ProductCategoryDto>.Failure("Category not found");

            category.Restore();
            await _unitOfWork.Categories.RestoreAsync(category);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result<IReadOnlyList<int>>> GetAssignedByProduct(int productId)
        {
            var categoryIds = await _unitOfWork.Products.GetAssignedCategoryIdsAsync(productId);

            if (categoryIds == null || categoryIds.Count == 0)
                return Result<IReadOnlyList<int>>.Failure("No se encontraron categorías asignadas.");

            return Result<IReadOnlyList<int>>.Success(categoryIds);
        }

    }
}
