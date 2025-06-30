using Sales.Application.DTOs.Table;
using SharedKernel.Util;

namespace Sales.Application.UseCases
{
    public interface ITableService
    {
        Task<Result<IReadOnlyList<TableDto>>> GetAllAsync();
        Task<Result<IReadOnlyList<TableDto>>> GetActivesAsync();
        Task<Result<TableDto>> GetByIdAsync(int tableId);
        Task<Result<IReadOnlyList<TableDto>>> SearchByNumberAsync(string number);
        Task<Result<int>> CreateTableAsync(CreateTableDto request);
        Task<Result> MarkAsOccupedAsync(int tableId);
        Task<Result> MarkAsAvailableAsync(int tableId);
        Task<Result> DeleteTableAsync(int tableId);
        Task<Result> RestoreTableAsync(int tableId);
    }
}
