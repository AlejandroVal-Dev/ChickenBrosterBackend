using Sales.Application.DTOs.Table;
using Sales.Application.UseCases;
using Sales.Application.Util;
using Sales.Domain.Entities;
using SharedKernel.Util;

namespace Sales.Application.Services
{
    public class TableService : ITableService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IReadOnlyList<TableDto>>> GetAllAsync()
        {
            var tables = await _unitOfWork.Tables.GetAllAsync();
            var result = tables.Select(t => new TableDto
            {
                Id = t.Id,
                Number = t.Number,
                IsAvailable = t.IsAvailable,
                IsActive = t.IsActive,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<TableDto>>.Success(result);
        }

        public async Task<Result<IReadOnlyList<TableDto>>> GetActivesAsync()
        {
            var tables = await _unitOfWork.Tables.GetActivesAsync();
            var result = tables.Select(t => new TableDto
            {
                Id = t.Id,
                Number = t.Number,
                IsAvailable = t.IsAvailable,
                IsActive = t.IsActive,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<TableDto>>.Success(result);
        }

        public async Task<Result<TableDto>> GetByIdAsync(int tableId)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(tableId);
            if (table is null)
                return Result<TableDto>.Failure("Table not found");

            var dto = new TableDto
            {
                Id = table.Id,
                Number = table.Number,
                IsAvailable = table.IsAvailable,
                IsActive = table.IsActive,
                CreatedAt = table.CreatedAt,
                UpdatedAt = table.UpdatedAt
            };

            return Result<TableDto>.Success(dto);
        }

        public async Task<Result<IReadOnlyList<TableDto>>> SearchByNumberAsync(string number)
        {
            var tables = await _unitOfWork.Tables.SearchByNumberAsync(number);
            var result = tables.Select(t => new TableDto
            {
                Id = t.Id,
                Number = t.Number,
                IsAvailable = t.IsAvailable,
                IsActive = t.IsActive,
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();

            return Result<IReadOnlyList<TableDto>>.Success(result);
        }

        public async Task<Result<int>> CreateTableAsync(CreateTableDto request)
        {
            if (await _unitOfWork.Tables.ExistsByNumberAsync(request.Number))
                return Result<int>.Failure("Table with same number exists.");

            var table = new Table(
                request.Number
            );

            await _unitOfWork.Tables.AddAsync(table);
            await _unitOfWork.CommitAsync();

            return Result<int>.Success(table.Id);
        }

        public async Task<Result> MarkAsOccupedAsync(int tableId)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(tableId);
            if (table is null)
                return Result.Failure("Table not found");

            table.MarkAsOccupied();
            await _unitOfWork.Tables.UpdateAsync(table);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> MarkAsAvailableAsync(int tableId)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(tableId);
            if (table is null)
                return Result.Failure("Table not found");

            table.MarkAsAvailable();
            await _unitOfWork.Tables.UpdateAsync(table);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> DeleteTableAsync(int tableId)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(tableId);
            if (table is null)
                return Result.Failure("Table not found");

            table.Delete();
            await _unitOfWork.Tables.DeactivateAsync(table);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

        public async Task<Result> RestoreTableAsync(int tableId)
        {
            var table = await _unitOfWork.Tables.GetByIdAsync(tableId);
            if (table is null)
                return Result.Failure("Table not found");

            table.Restore();
            await _unitOfWork.Tables.RestoreAsync(table);
            await _unitOfWork.CommitAsync();

            return Result.Success();
        }

    }
}
