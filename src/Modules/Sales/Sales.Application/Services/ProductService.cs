using Sales.Application.DTOs.Product;
using Sales.Application.DTOs.ProductCategoryAssignment;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllAsync()
        {
            var products = await _unitOfWork.Products.GetAllAsync();
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetActivesAsync()
        {
            var products = await _unitOfWork.Products.GetActivesAsync();
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(result);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
                return Result<ProductDto>.Failure("Product not found");

            var dto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };

            return Result<ProductDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetByCategoryIdAsync(int categoryId)
        {
            var products = await _unitOfWork.Products.GetByCategoryAsync(categoryId);
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> SearchByNameAsync(string name)
        {
            var products = await _unitOfWork.Products.SearchByNameAsync(name);
            var result = products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<ProductDto>>.Success(result);
        }

        public async Task<Result<int>> CreateProductAsync(CreateProductDto request)
        {
            if (await _unitOfWork.Products.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("Product with same number exists.");

            var product = new Product(
                name: request.Name,
                description: request.Description,
                price: request.Price
            );

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(product.Id);
        }

        public async Task<Result> UpdateProductAsync(UpdateProductDto request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure("Product not found");

            product.UpdateInformation(request.NewName, request.NewDescription, request.NewPrice);
            await _unitOfWork.Products.DeactivateAsync(product);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteProductAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
                return Result.Failure("Product not found");

            product.Delete();
            await _unitOfWork.Products.DeactivateAsync(product);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreProductAsync(int productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product is null)
                return Result.Failure("Product not found");

            product.Restore();
            await _unitOfWork.Products.RestoreAsync(product);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> AssignCategoryAsync(ProductCategoryAssignmentDto request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure("Product not found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);

            if (category is null)
                return Result.Failure("Category not found.");

            var exist = await _unitOfWork.Products.GetAssignedCategoryIdsAsync(product.Id);
            if (exist.Contains(category.Id))
                return Result.Failure($"{product.Name} is already assigned to {category.Name}");

            var assignment = new ProductCategoryAssignment(request.ProductId, request.CategoryId);
            await _unitOfWork.Products.AssignCategoryAsync(assignment);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> UnassignCategoryAsync(ProductCategoryAssignmentDto request)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(request.ProductId);
            if (product is null)
                return Result.Failure("Product not found.");

            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category is null)
                return Result.Failure("Category not found.");

            var exist = await _unitOfWork.Products.GetAssignedCategoryIdsAsync(product.Id);
            if (!exist.Contains(category.Id))
                return Result.Failure($"{product.Name} is not assigned to {category.Name}");

            var unassignment = new ProductCategoryAssignment(request.ProductId, request.CategoryId);
            await _unitOfWork.Products.UnassignCategoryAsync(unassignment);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

    }
}
