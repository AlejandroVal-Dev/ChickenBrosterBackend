using Inventory.Application.DTOs.IngredientCategory;
using Inventory.Application.DTOs.UnitOfMeasure;
using Inventory.Application.UseCases;
using Inventory.Application.Util;
using Inventory.Domain.Entities;
using SharedKernel.Util;

namespace Inventory.Application.Services
{
    public class UnitOfMeasureService : IUnitOfMeasureService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitOfMeasureService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<UnitOfMeasureDto>>> GetAllAsync()
        {
            var units = await _unitOfWork.UnitsOfMeasure.GetAllAsync();
            var result = units.Select(u => new UnitOfMeasureDto 
            {
                Id = u.Id,
                Name = u.Name,
                Abbreviation = u.Abbreviation,
                IsDecimal = u.IsDecimal,
                ConversionFactor = u.ConversionFactor,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<UnitOfMeasureDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<UnitOfMeasureDto>>> GetActivesAsync()
        {
            var units = await _unitOfWork.UnitsOfMeasure.GetActivesAsync();
            var result = units.Select(u => new UnitOfMeasureDto
            {
                Id = u.Id,
                Name = u.Name,
                Abbreviation = u.Abbreviation,
                IsDecimal = u.IsDecimal,
                ConversionFactor = u.ConversionFactor,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<UnitOfMeasureDto>>.Success(result);
        }

        public async Task<Result<UnitOfMeasureDto>> GetByIdAsync(int id)
        {
            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(id);
            if (unit is null)
                return Result<UnitOfMeasureDto>.Failure("UnitOfMeasure not found.");

            var dto = new UnitOfMeasureDto
            {
                Id = unit.Id,
                Name = unit.Name,
                Abbreviation = unit.Abbreviation,
                IsDecimal = unit.IsDecimal,
                ConversionFactor = unit.ConversionFactor,
                IsActive = unit.IsActive,
                CreatedAt = unit.CreatedAt,
                UpdatedAt = unit.UpdatedAt
            };

            return Result<UnitOfMeasureDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<UnitOfMeasureDto>>> SearchByNameAsync(string name)
        {
            var units = await _unitOfWork.UnitsOfMeasure.SearchByNameAsync(name);
            var result = units.Select(u => new UnitOfMeasureDto
            {
                Id = u.Id,
                Name = u.Name,
                Abbreviation = u.Abbreviation,
                IsDecimal = u.IsDecimal,
                ConversionFactor = u.ConversionFactor,
                IsActive = u.IsActive,
                CreatedAt = u.CreatedAt,
                UpdatedAt = u.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<UnitOfMeasureDto>>.Success(result);
        }

        public async Task<Result<int>> CreateAsync(CreateUnitOfMeasureDto request)
        {
            if (await _unitOfWork.UnitsOfMeasure.ExistsByNameAsync(request.Name))
                return Result<int>.Failure("UnitOfMeasure with same name exists.");

            var unit = new UnitOfMeasure(
                request.Name,
                request.Abbreviation,
                request.IsDecimal,
                request.ConversionFactor
            );

            await _unitOfWork.UnitsOfMeasure.AddAsync(unit);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(unit.Id);
        }

        public async Task<Result> UpdateAsync(UpdateUnitOfMeasureDto request)
        {
            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(request.UnitOfMeasureId);
            if (unit is null)
                return Result.Failure("UnitOfMeasure not found.");

            unit.UpdateInformation(request.Name, request.Abbreviation, request.IsDecimal, request.ConversionFactor);
            await _unitOfWork.UnitsOfMeasure.UpdateAsync(unit);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(int id)
        {
            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(id);
            if (unit is null)
                return Result.Failure("UnitOfMeasure not found.");

            unit.Delete();
            await _unitOfWork.UnitsOfMeasure.DeactivateAsync(unit);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreAsync(int id)
        {
            var unit = await _unitOfWork.UnitsOfMeasure.GetByIdAsync(id);
            if (unit is null)
                return Result.Failure("UnitOfMeasure not found.");

            unit.Restore();
            await _unitOfWork.UnitsOfMeasure.RestoreAsync(unit);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }
    }
}
